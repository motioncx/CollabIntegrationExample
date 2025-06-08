using System.Net.Http.Headers;

namespace HttpClient.RefitProxy.Auth;

public class AuthApiKeySettings
    {
        public string Token { get; set; }
    }

    internal static class AuthHeaderBase
    {
        public static void AddAuthApiKeyHeader(HttpRequestMessage request, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("x-api-key", token);
            }
        }
        
        public static void AddAuthBearerHeader(HttpRequestMessage request, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                token = token.Replace("Bearer ", string.Empty);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }

    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly AuthApiKeySettings _authApiKeySettings;

        public AuthHeaderHandler(AuthApiKeySettings authApiKeySettings)
        {
            _authApiKeySettings = authApiKeySettings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthHeaderBase.AddAuthApiKeyHeader(request, _authApiKeySettings.Token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
    
    public class BearerAuthHeaderHandler : DelegatingHandler
    {
        private readonly string _token;

        public BearerAuthHeaderHandler(string token, HttpMessageHandler messageHandler)
            : base(messageHandler)
        {
            _token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthHeaderBase.AddAuthBearerHeader(request, _token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
   