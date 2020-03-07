using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notification.API.Interfaces;
using Notification.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notification.Api
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _templateUri;
        private readonly string _sendGridApiKey;
        private readonly HttpClient _httpClient;

        public NotificationService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _templateUri = _configuration["TemplateApi:Url"];
            _sendGridApiKey = _configuration["SendGrid:ApiKey"];
            _httpClient = httpClient;
        }

        public async Task<TemplateResponse> GetTemplates(int branchId, string type, Method method)
        {
            var uri = $"{_templateUri}/{branchId}";

            if (!string.IsNullOrEmpty(type))
            {
                uri += $"/{type}";
            }

            if (method != 0)
            {
                uri += $"/{method}";
            }

            var response = await _httpClient.GetAsync(uri);
            var result = response.Content != null ? 
                response.Content.ReadAsStringAsync().Result : 
                response.StatusCode.ToString();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Notification Templates API Error:{response.StatusCode}.{response.Content.ReadAsStringAsync().Result}");
            }

            var templates = JsonConvert.DeserializeObject<List<NotificationTemplate>>(result);
            return new TemplateResponse() { NotificationTemplates = templates };
        }

        public async Task<NotificationResponse> Send(NotificationRequest request, NotificationTemplate template)
        {
            var fromAddress = template.EmailAddress;
            var fromName = template.SenderName;
            var templateId = template.TemplateId;
            var toName = request.CustomerId;
            var toAddress = template.NotificationMethod == Method.SMS ?
                GetFullSMS("64", request.CustomerMobile, template.SMSDomain) :
                request.CustomerEmail;

            var mailResponse = await SendGridEmail(fromAddress, fromName, toAddress, toName, templateId, request);

            var response = new NotificationResponse
            {
                TransactionId = request.TransactionId,
                OrderNo = request.OrderNo,
                ServiceResult = new ServiceResult()
                {
                    Code = mailResponse.Success ? 0 : 1,
                    Message = string.IsNullOrEmpty(mailResponse.Message) ? "Success" : mailResponse.Message
                }
            };

            return response;
        }

        private async Task<MailResponse> SendGridEmail(string fromAddress, string fromName, string toAddress, string toName, string templateId, NotificationRequest notificationRequest)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(fromAddress, fromName),
                TemplateId = templateId,
                Personalizations = new List<SendGrid.Helpers.Mail.Personalization>
                {
                    new Models.Personalization {
                        Tos = new List<EmailAddress> { new EmailAddress(toAddress, toName) },
                        TemplateData = notificationRequest
                    }
                }
            };

            var client = new SendGridClient(_sendGridApiKey);
            var response = await client.SendEmailAsync(message);
            var result = response.Body.ReadAsStringAsync().Result;
            return new MailResponse(response.StatusCode == HttpStatusCode.Accepted, result);
        }

        private string GetFullSMS(string countryPrefix, string mobile, string domain)
        {
            return $"+{countryPrefix}{mobile.TrimStart('0')}@{domain}";
        }
    }
}
