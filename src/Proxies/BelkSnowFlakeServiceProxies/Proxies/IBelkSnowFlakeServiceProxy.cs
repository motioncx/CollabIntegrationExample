using Newtonsoft.Json.Linq;
using Refit;

namespace BelkSnowFlakeServiceProxies.Proxies;

public interface IBelkSnowFlakeServiceProxy
{
    
    
    [Get("/stores/order-lookup/v1/order/{id}")]
    Task<JObject> GetOrderById(string id);
    
    
    [Get("/stores/receipts/order/{id}")]
    Task<byte[]> GetReceiptByOrderId(string id);
    
    [Get("/stores/order-lookup/v1/orders?emailId={orderId}&fromDate=&toDate=")]
    Task<JObject> GetOrderByEmail(string orderId);

    [Get("/stores/order-lookup/v1/orders?phoneNo={orderId}&fromDate=&toDate=")]
    Task<JObject> GetOrdersByPhone(string orderId);
}
