using Abp.Domain.Services;

namespace Sixoclock.Onyx
{
    public abstract class OnyxDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected OnyxDomainServiceBase()
        {
            LocalizationSourceName = OnyxConsts.LocalizationSourceName;
        }
    }
}
