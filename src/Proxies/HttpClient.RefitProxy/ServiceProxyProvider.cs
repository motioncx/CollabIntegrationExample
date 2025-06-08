using HttpClient.RefitProxy.Auth;
using HttpClient.RefitProxy.Extensions;
using Refit;

namespace HttpClient.RefitProxy;

public class ServiceProxyProvider<T> where T : class
{


    public T GetServiceProxy(string authorization, string urlOverride, List<(string key, string val)> headers = null, Action<System.Net.Http.HttpClient> httpClientCallback = null)
    {
        var httpClient = new System.Net.Http.HttpClient(new BearerAuthHeaderHandler(authorization, new HttpClientHandler()));
        var method = RefitExtensions.SetupHttpClient(urlOverride, headers, httpClientCallback);
        method(httpClient);

        var serviceProxy = RestService.For<T>(httpClient);
        return serviceProxy;
    }
}