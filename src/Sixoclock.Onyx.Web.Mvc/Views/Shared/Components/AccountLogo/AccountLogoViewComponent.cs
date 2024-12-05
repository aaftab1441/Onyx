using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Web.Session;

namespace Sixoclock.Onyx.Web.Views.Shared.Components.AccountLogo
{
    public class AccountLogoViewComponent : OnyxViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AccountLogoViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
            return View(new AccountLogoViewModel(loginInfo));
        }
    }
}
