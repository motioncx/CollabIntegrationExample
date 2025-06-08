using BelkSnowFlakeServiceProxies.Proxies;
using HttpClient.RefitProxy;
using Microsoft.AspNetCore.Mvc;

namespace CollabIntegrationExample.WebApp.Controllers;


[ApiController]
[Route("belk/stores/")]
public class BelkStoresApiController(IBelkSnowFlakeServiceProxy proxy): ControllerBase
{
    [HttpGet]
    [Route("order-lookup/v1/order-id")]
    /*
       curl -X 'GET' \
       'https://localhost:7291/belk/stores/order-lookup/v1/order-id?orderId=01200115203409262024' \
       -H 'accept: * /*'
     */
    public async Task<IActionResult> GetByOrderId(string orderId)
    {
        //01200115203409262024

        try
        {
          
            var result = await proxy.GetOrderById(orderId);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
                
            return BadRequest( $"Exception occurred attempting to search order id:{orderId}.  Exceoption message: " + ex.Message + ", StackException: " + ex.StackTrace);
        }
    }
        
    
    [HttpGet]
    /*
       curl -X 'GET' \
       'https://localhost:7291/belk/stores/order-lookup/v1/order?storeId=0120&registerNumber=0115&transactionNumber=2034&month=09&day=26&year=2024' \
       -H 'accept: * /*'
     */
    [Route("order-lookup/v1/order-id-args")]
    public async Task<IActionResult> Get(int storeId, int registerNumber, int transactionNumber, int month, int day, int year)
    {
            // i believe this only keeps it around 90 days or so, so ahrd for me to fully test this
            
            //https://apps-qa.belk.com/stores/order-lookup/v1/order/00010001217909242024
            string requestId = "";
            try
            {
                var storeIdPadded = HelperPadding.Pad(storeId, 4);
                var registerNumberPadded = HelperPadding.Pad(registerNumber, 4);
                var transactionNumberPadded = HelperPadding.Pad(transactionNumber, 4);
                var monthPadded = HelperPadding.Pad(month, 2);
                var dayPadded = HelperPadding.Pad(day, 2);
                var yearPadded = HelperPadding.Pad(year, 4);
                requestId = $"{storeIdPadded}{registerNumberPadded}{transactionNumberPadded}{monthPadded}{dayPadded}{yearPadded}";

                var result = await proxy.GetOrderById(requestId);
        
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return BadRequest( $"Exception occurred attempting to search order id:{requestId}.  Exceoption message: " + ex.Message + ", StackException: " + ex.StackTrace);
            }
    }
    
    [HttpGet]
    [Route("order-lookup/v1/by-email")]
    /*
   curl -X 'GET' \
       'https://localhost:7291/belk/stores/order-lookup/v1/by-email?email=CBPARKER47%40GMAIL.COM' \
       -H 'accept: * /*'  
     */
    public async Task<IActionResult> GetOrdersByEmail(string email)
    {
        try
        {

            var result = await proxy.GetOrderByEmail(email);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
                
            return BadRequest( $"Exception occurred attempting to search orders email:{email}.  Exceoption message: " + ex.Message + ", StackException: " + ex.StackTrace);
        }
    }
    
    [HttpGet]
    [Route("order-lookup/v1/by-phone")]
    /*
        curl -X 'GET' \
       'https://localhost:7291/belk/stores/order-lookup/v1/by-phone?phone=9107337141' \
       -H 'accept: * /*'
     */
    public async Task<IActionResult> GetOrderByPhone(string phone)
    {
        try
        {
            var result = await proxy.GetOrdersByPhone(phone);
        
            return Ok(result);
        }
        catch (Exception ex)
        {
                
            return BadRequest( $"Exception occurred attempting to search orders phone:{phone}.  Exceoption message: " + ex.Message + ", StackException: " + ex.StackTrace);
        }
    }
    
    
    [HttpGet]
    [Route("order-lookup/v1/order/receipt")]
    public async Task<IActionResult> GetOrderReceipt(string orderId)
    {
        try
        {
                 
            var original = await proxy.GetReceiptByOrderId(orderId);
            Response.Clear();
            Response.ContentType = "image/png";
        
        
            //return "data:image/png;base64," + Convert.ToBase64String(original);
            MemoryStream ms = new MemoryStream(original);
            return new FileStreamResult(ms, "image/png");
            
        }
        catch (Exception ex)
        {
                
            return BadRequest( $"Exception occurred attempting to retrieve receipt:{orderId}.  Exceoption message: " + ex.Message + ", StackException: " + ex.StackTrace);
        }
    }
}