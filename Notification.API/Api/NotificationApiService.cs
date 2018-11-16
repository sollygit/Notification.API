using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notification.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Notification.Api
{
    public class NotificationApiService
    {
        readonly IConfiguration _configuration;
        readonly string _templateUri;
        readonly string _sendGridApiKey;
        readonly int _timeout = 20;

        public NotificationApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _templateUri = _configuration["TemplateApi:Url"];
            _sendGridApiKey = _configuration["SendGrid:ApiKey"];
        }

        public async Task<HttpResponseMessage> GetNotificationTemplatesAsync(int branchId, string type, Method method)
        {
            using (var _client = new HttpClient())
            {
                _client.Timeout = TimeSpan.FromSeconds(_timeout);
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var uri = $"{_templateUri}/{branchId}";

                if (!string.IsNullOrEmpty(type))
                {
                    uri += $"/{type}";
                }

                if (method != 0)
                {
                    uri += $"/{method}";
                }

                var response = await _client.GetAsync(uri);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Notification Templates API Error:{response.StatusCode}.{response.Content.ReadAsStringAsync().Result}");
                }

                return response;
            }
        }

        public async Task<MailResponse> SendGridEmailAsync(string fromAddress, string fromName, string toAddress, string toName, string templateId, NotificationRequest notificationRequest)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(fromAddress, fromName),
                TemplateId = templateId,
                Personalizations = new List<SendGrid.Helpers.Mail.Personalization>
                {
                    new Personalization {
                        Tos = new List<EmailAddress> { new EmailAddress(toAddress, toName) },
                        TemplateData = notificationRequest
                    }
                }
            };

            var client = new SendGridClient(_sendGridApiKey);
            var response = await client.SendEmailAsync(message);
            
            return new MailResponse(response.StatusCode == HttpStatusCode.Accepted, response.Body.ReadAsStringAsync().Result);
        }
    }

    class Personalization : SendGrid.Helpers.Mail.Personalization
    {
        [JsonProperty("dynamic_template_data")]
        public NotificationRequest TemplateData { get; set; }
    }
}
