using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notification.Api;
using Notification.API.Interfaces;
using Notification.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.API.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        readonly IConfiguration _configuration;

        public NotificationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TemplateResponse> GetNotificationTemplatesAsync(int branchId, string type, Method method)
        {
            var service = new NotificationApiService(_configuration);
            var response = await service.GetNotificationTemplatesAsync(branchId, type, method);
            var result = response.Content != null ? response.Content.ReadAsStringAsync().Result : response.StatusCode.ToString();

            return new TemplateResponse()
            {
                NotificationTemplates = JsonConvert.DeserializeObject<List<NotificationTemplate>>(result)
            };
        }

        public async Task<NotificationResponse> SendNotificationAsync(NotificationRequest request, NotificationTemplate template)
        {
            var service = new NotificationApiService(_configuration);
            var fromAddress = template.EmailAddress;
            var fromName = template.SenderName;
            var templateId = template.TemplateId;
            var toName = request.CustomerId;
            var toAddress = template.NotificationMethod == Method.SMS ?
                GetFullSMS("64", request.CustomerMobile, template.SMSDomain) :
                request.CustomerEmail;

            var mailResponse = await service.SendGridEmailAsync(fromAddress, fromName, toAddress, toName, templateId, request);

            var response = new NotificationResponse {
                TransactionId = request.TransactionId,
                OrderNo = request.OrderNo,
                ServiceResult = new ServiceResult() {
                    Code = mailResponse.Success ? 0 : 1,
                    Message = string.IsNullOrEmpty(mailResponse.Message) ? "Success" : mailResponse.Message
                }
            };

            return response;
        }

        private string GetFullSMS(string countryPrefix, string mobile, string domain)
        {
            return $"+{countryPrefix}{mobile.TrimStart('0')}@{domain}";
        }
    }
}
