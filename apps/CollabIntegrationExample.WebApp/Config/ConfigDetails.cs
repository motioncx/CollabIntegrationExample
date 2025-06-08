using Newtonsoft.Json;

namespace CollabIntegrationExample.WebApp.Config;

public class ConfigDetails
{
    public class BelkUserPass
    {
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class ApiDetails
    {
        public BelkUserPass BelkStore { get; set; }
        public BelkUserPass BelkECom { get; set; }
        public string MotionApiKey { get; set; }
        public string MotionApiUrl { get; set; }
        public string BelkBaseUrl { get; set; }   
    }
    
    public class ApiSettings
    {
        public ApiDetails UAT { get; set; }
        public ApiDetails PROD { get; set; }

     
    }

    public class GetAPIs
    {
        [JsonProperty(PropertyName = "APIs")]
        public ApiSettings APIs { get; set; }
        
        public static ApiSettings ReadFromJsonFile(IConfiguration config)
        {
            return config.Get<GetAPIs>().APIs;
        }

    }

}