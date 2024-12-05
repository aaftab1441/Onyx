using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Public.Controllers
{
    public class HomeController : OnyxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}