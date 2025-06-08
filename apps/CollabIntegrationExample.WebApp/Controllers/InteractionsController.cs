
using CollabIntegrationExample.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
namespace CollabIntegrationExample.WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class InteractionsController(
        CrmServiceProxy crmServiceProxy, 
        InteractionServiceProxy interactionServiceProxy
        ): ControllerBase
{
    
    private InteractionServiceProxy _interactionServiceProxy = interactionServiceProxy;
    


    [HttpGet]
    [Route("get-tickets")]
    public async Task<IActionResult> GetTranscript(Guid interactionId)
    {
        var trasnscript = await _interactionServiceProxy.GetTranscript(interactionId);
        return Ok(trasnscript);
    }
    

}