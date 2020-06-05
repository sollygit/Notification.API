using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Notification.Models
{
    public class NotificationRequest
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }
        [JsonProperty("branchId")]
        public int BranchId { get; set; }
        [JsonProperty("branchName")]
        public string BranchName { get; set; }
        [JsonProperty("phoneNo")]
        public string PhoneNo { get; set; }
        [JsonProperty("address1")]
        public string Address1 { get; set; }
        [JsonProperty("address2")]
        public string Address2 { get; set; }
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }
        [JsonProperty("customerMobile")]
        public string CustomerMobile { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("notificationType")]
        public string NotificationType { get; set; }
        [JsonProperty("fulfilmentType")]
        public JObject FulfilmentType { get; set; }
        [JsonProperty("notificationMethod")]
        public Method NotificationMethod { get; set; }
        [JsonProperty("requestDate")]
        public string RequestDate { get; set; }
        [JsonProperty("orderLocation")]
        public string OrderLocation { get; set; }
        [JsonProperty("dsp")]
        public JObject DSP { get; set; }
        [JsonProperty("eta")]
        public string ETA { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }
}
