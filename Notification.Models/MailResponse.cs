using System;

namespace Notification.Models
{
    public class MailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public MailResponse()
        {
        }

        public MailResponse(bool success, string message = "", Exception exception = null)
        {
            Success = success;
            Message = message;
            Exception = exception;
        }
    }
}
