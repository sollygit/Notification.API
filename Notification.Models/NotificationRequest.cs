using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Notification.Models
{
    public class NotificationRequest
    {
        public string TransactionId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string PhoneNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string OrderNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string Subject { get; set; }
        public string NotificationType { get; set; }
        public JObject FulfilmentType { get; set; }
        public Method NotificationMethod { get; set; }
        public string RequestDate { get; set; }
        public string OrderLocation { get; set; }
        public JObject DSP { get; set; }
        public string ETA { get; set; }
        public string Time { get; set; }
        public List<Product> Products { get; set; }
    }
}
