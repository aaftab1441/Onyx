using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.API.Json.Controllers
{
    public class HomeController:OnyxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
