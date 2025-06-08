
using MotionServiceProxies.HttpProxy;
using Shared.Models.IR;
using Telemetry;

namespace CollabIntegrationExample.WebApp.Services;

public class InteractionServiceProxy 
{


    private ILogger<InteractionServiceProxy> _logger;
    private IHttpContextAccessor _httpContext;

    private IInteractionServiceProxy _interactionServiceProxy;


    public InteractionServiceProxy(
        IInteractionServiceProxy interactionServiceProxy, 
        IHttpContextAccessor httpContext, 
        ILogger<InteractionServiceProxy> logger) 
        
    {
        _interactionServiceProxy = interactionServiceProxy;
        _logger = logger;
        _httpContext = httpContext;

    }

    public async Task<TranscriptResult?>  GetTranscript(Guid interactionId)
    {
        
        var resp = await _interactionServiceProxy.TranscriptsAsync(interactionId, false);
        if (!resp.IsSuccessStatusCode)
        {
            var errorMessage = "Unable to process [{InteractionId}]:" + interactionId + ", as failed to retrieve transcript!";
            MotionLog.Error(errorMessage);
            throw new Exception(errorMessage);
        }

        var transcriptResult = resp.Content;
        
        return transcriptResult;

    }

}