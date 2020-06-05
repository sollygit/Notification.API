namespace Notification.Models
{
    public class NotificationTemplate
    {
        public string SMSDomain { get; set; }
        public string SenderName { get; set; }
        public string EmailAddress { get; set; }
        public string NotificationType { get; set; }
        public Method NotificationMethod { get; set; }
        public string TemplateId { get; set; }
    }
}
