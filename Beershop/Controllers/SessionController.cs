using Beershop.Extensions;
using Beershop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beershop.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            // HttpContext.Session.Set("test", only array of bytes);
            // write our own extentionMethode

            HttpContext.Session.SetObject("mySession",
                new SessionVM { Date = DateTime.Now, Company = "VIVES"});
            return View();
        }
    }
}
