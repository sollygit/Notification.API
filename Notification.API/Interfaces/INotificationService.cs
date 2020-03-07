using Notification.Models;
using System.Threading.Tasks;

namespace Notification.API.Interfaces
{
    public interface INotificationService
    {
        Task<TemplateResponse> GetTemplates(int branchId, string type, Method method);
        Task<NotificationResponse> Send(NotificationRequest request, NotificationTemplate template);
    }
}
