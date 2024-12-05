using Microsoft.AspNetCore.Antiforgery;

namespace Sixoclock.Onyx.Web.Controllers
{
    public class AntiForgeryController : OnyxControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
