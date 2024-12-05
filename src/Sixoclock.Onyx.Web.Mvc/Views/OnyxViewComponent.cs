using Abp.AspNetCore.Mvc.ViewComponents;

namespace Sixoclock.Onyx.Web.Views
{
    public abstract class OnyxViewComponent : AbpViewComponent
    {
        protected OnyxViewComponent()
        {
            LocalizationSourceName = OnyxConsts.LocalizationSourceName;
        }
    }
}