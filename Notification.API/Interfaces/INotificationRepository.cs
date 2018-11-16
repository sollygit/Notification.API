using Notification.Models;
using System.Threading.Tasks;

namespace Notification.API.Interfaces
{
    public interface INotificationRepository
    {
        Task<TemplateResponse> GetNotificationTemplatesAsync(int branchId, string type, Method method);
        Task<NotificationResponse> SendNotificationAsync(NotificationRequest request, NotificationTemplate template);
    }
}
