using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Areas.App.Models.Layout;
using Sixoclock.Onyx.Web.Session;
using Sixoclock.Onyx.Web.Views;

namespace Sixoclock.Onyx.Web.Areas.App.Views.Shared.Components.AppFooter
{
    public class AppFooterViewComponent : OnyxViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppFooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
