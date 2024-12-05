using Abp.AspNetCore.Mvc.ViewComponents;

namespace Sixoclock.Onyx.Web.Public.Views
{
    public abstract class OnyxViewComponent : AbpViewComponent
    {
        protected OnyxViewComponent()
        {
            LocalizationSourceName = OnyxConsts.LocalizationSourceName;
        }
    }
}