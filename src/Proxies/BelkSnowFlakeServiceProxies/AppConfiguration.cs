using System.Text;
using BelkSnowFlakeServiceProxies.Proxies;
using BelkSnowFlakeServiceProxies.Proxies;
using HttpClient.RefitProxy.Auth;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HttpClient.RefitProxy.Extensions;
using Refit;

namespace BelkSnowFlakeServiceProxies;

public static class AppConfiguration
{
    public static IServiceCollection SetupSnowflakeProxies(this IServiceCollection serviceCollection, string authUsername, string authPassword, string url = "https://apps-qa.belk.com")
    {

        
        List<(string key, string val)> headers = new List<(string key, string val)>();
        string encoded = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(authUsername + ":" + authPassword));
        headers.Add(new ValueTuple<string, string>("Authorization", "Basic " + encoded));
        
        
        serviceCollection
            .AddTransient<AuthHeaderHandler>()
            .AddSingleton(new AuthApiKeySettings
            {
                // due to them using basic auth, we need to override and not add a token, just add headers above
                // Token = encoded,
            });

        
        var camel = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                }
            )
        };
        // Admin service proxies
        serviceCollection.SetupRefitClient<IBelkSnowFlakeServiceProxy>( camel, url, headers);


        return serviceCollection;
    }
}