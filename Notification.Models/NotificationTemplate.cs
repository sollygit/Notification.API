using Newtonsoft.Json;

namespace Notification.Models
{
    public class NotificationTemplate
    {
        [JsonProperty("smsDomain")]
        public string SMSDomain { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("notificationType")]
        public string NotificationType { get; set; }

        [JsonProperty("notificationMethod")]
        public Method NotificationMethod { get; set; }

        [JsonProperty("templateId")]
        public string TemplateId { get; set; }
    }
}
