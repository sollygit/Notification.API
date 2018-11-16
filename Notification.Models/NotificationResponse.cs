namespace Notification.Models
{
    public class NotificationResponse
    {
        public string TransactionId { get; set; }
        public string OrderNo { get; set; }
        public ServiceResult ServiceResult { get; set; }
    }
}
