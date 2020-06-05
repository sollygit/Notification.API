using Microsoft.AspNetCore.Mvc;

namespace Notification.API.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectPermanent("/swagger/index.html");
        }
    }
}
