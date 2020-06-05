using Newtonsoft.Json;

namespace Notification.Models
{
    public class NotificationResponse
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }
        [JsonProperty("serviceResult")]
        public ServiceResult ServiceResult { get; set; }
    }
}
