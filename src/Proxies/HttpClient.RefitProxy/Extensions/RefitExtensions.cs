using HttpClient.RefitProxy.Auth;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace HttpClient.RefitProxy.Extensions;

public static class RefitExtensions
{
    public static IServiceCollection SetupRefitClient<T>(this IServiceCollection serviceCollection, 
        RefitSettings settings = null,
        string urlOverride = null,
        List<(string key, string val)> headers = null,
        Action<System.Net.Http.HttpClient> httpClientCallback = null
    ) where T : class
    {
        var builder = GetHttpClientBuilder<T>(serviceCollection,settings, urlOverride, headers, httpClientCallback);

        builder.AddHttpMessageHandler<AuthHeaderHandler>();


        return serviceCollection;
    }

    public static IHttpClientBuilder GetHttpClientBuilder<T>(IServiceCollection serviceCollection, 
        RefitSettings settings, string urlOverride = null, List<(string key, string val)> headers = null,  Action<System.Net.Http.HttpClient> httpClientCallback = null) where T : class
    {
        var builder = serviceCollection
            .AddRefitClient<T>(settings)
            .ConfigureHttpClient(SetupHttpClient(urlOverride, headers, httpClientCallback));

        serviceCollection.AddSingleton(new ServiceProxyProvider<T>());

        return builder;
    }

    public static Action<System.Net.Http.HttpClient> SetupHttpClient( string urlOverride, List<(string key, string val)> headers = null,  Action<System.Net.Http.HttpClient> httpClientCallback = null)
    {
        return c =>
        {
            foreach (var h in headers)
            {
                c.DefaultRequestHeaders.TryAddWithoutValidation(h.key, h.val);
            }
            c.BaseAddress = new Uri(urlOverride);
            httpClientCallback?.Invoke(c);
        };
    }
}