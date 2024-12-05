using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Configuration;
using Sixoclock.Onyx.Web.Areas.App.Models.Layout;
using Sixoclock.Onyx.Web.Session;
using Sixoclock.Onyx.Web.Views;

namespace Sixoclock.Onyx.Web.Areas.App.Views.Shared.Components.AppHeader
{
    public class AppHeaderViewComponent : OnyxViewComponent
    {
        private readonly ILanguageManager _languageManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IAbpSession _abpSession;

        public AppHeaderViewComponent(
            IMultiTenancyConfig multiTenancyConfig, 
            IAbpSession abpSession,
            ILanguageManager languageManager, 
            IPerRequestSessionCache sessionCache)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _abpSession = abpSession;
            _languageManager = languageManager;
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList(),
                CurrentLanguage = _languageManager.CurrentLanguage,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsImpersonatedLogin = _abpSession.ImpersonatorUserId.HasValue,
                SubscriptionExpireNootifyDayCount = SettingManager.GetSettingValue<int>(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount)
            };

            return View(headerModel);
        }
    }
}
