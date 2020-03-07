using Newtonsoft.Json;

namespace Notification.Models
{
    public class Personalization : SendGrid.Helpers.Mail.Personalization
    {
        [JsonProperty("dynamic_template_data")]
        public NotificationRequest TemplateData { get; set; }
    }
}
