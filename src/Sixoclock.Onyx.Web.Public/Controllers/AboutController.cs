using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Public.Controllers
{
    public class AboutController : OnyxControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}