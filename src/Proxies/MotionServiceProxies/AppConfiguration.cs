using System.Text;

using HttpClient.RefitProxy.Auth;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HttpClient.RefitProxy.Extensions;
using MotionServiceProxies.HttpProxy;
using Refit;

namespace MotionServiceProxies;

public static class AppConfiguration
{
    public static IServiceCollection SetupMotionProxies(this IServiceCollection serviceCollection, string apiKey, string url)
    {
        serviceCollection
            .AddTransient<AuthHeaderHandler>()
            .AddSingleton(new AuthApiKeySettings
            {
                Token = apiKey
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
        serviceCollection
            .SetupRefitClient<IInteractionServiceProxy>( camel, url)
            .SetupRefitClient<IReportingApiServiceProxy>( camel, url)
            .SetupRefitClient<ITicketServiceProxy>( camel, url)
            ;


        return serviceCollection;
    }
}