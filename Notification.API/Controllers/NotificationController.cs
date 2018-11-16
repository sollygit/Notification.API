using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Notification.API.Interfaces;
using Notification.API.Repository;
using Notification.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.API.Controllers
{
    [Route("api/cona/100/[controller]")]
    public class NotificationController : Controller
    {
        readonly IConfiguration _configuration;
        readonly INotificationRepository repository;

        public NotificationController(IConfiguration configuration)
        {
            _configuration = configuration;
            repository = new NotificationRepository(_configuration);
        }

        [HttpGet("NotificationTemplate")]
        public async Task<IActionResult> NotificationTemplate([FromQuery, BindRequired] int branchId, [FromQuery] string type, [FromQuery] Method method)
        {
            try
            {
                var templatesTask = await repository.GetNotificationTemplatesAsync(branchId, type, method);
                var templates = templatesTask.NotificationTemplates;

                return Ok(templates);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] NotificationRequest request)
        {
            if (request == null)
            {
                return BadRequest(new NotificationResponse {
                    TransactionId = request.TransactionId,
                    OrderNo = request.OrderNo,
                    ServiceResult = new ServiceResult { Code = 2, Message = "Request could not be parsed" }
                });
            }

            var response = new NotificationResponse { TransactionId = request.TransactionId, OrderNo = request.OrderNo };

            try
            {
                // Get a notification template by a given type and method
                var templatesTask = await repository.GetNotificationTemplatesAsync(request.BranchId, request.NotificationType, request.NotificationMethod);

                // Exit if Notification Template not found
                if (templatesTask.NotificationTemplates.Count == 0)
                {
                    return BadRequest(new NotificationResponse
                    {
                        TransactionId = request.TransactionId,
                        OrderNo = request.OrderNo,
                        ServiceResult = new ServiceResult
                        {
                            Code = 2,
                            Message = $"Notification Template of Type {request.NotificationType} and Method {request.NotificationMethod} could not be found."
                        }
                    });
                }

                // Get the template
                var template = templatesTask.NotificationTemplates.Where(o => o.NotificationMethod == request.NotificationMethod).FirstOrDefault();

                // Send a Notification
                var responseTask = await repository.SendNotificationAsync(request, template);

                response.ServiceResult = responseTask.ServiceResult;

                return Ok(response);
            }

            catch (Exception ex)
            {
                response.ServiceResult = new ServiceResult
                {
                    Code = 1,
                    Message = ex.Message
                };

                return StatusCode(500, response);
            }
        }
    }
}
