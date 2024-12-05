using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Sixoclock.Onyx.Web.Views
{
    public abstract class OnyxRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected OnyxRazorPage()
        {
            LocalizationSourceName = OnyxConsts.LocalizationSourceName;
        }
    }
}
