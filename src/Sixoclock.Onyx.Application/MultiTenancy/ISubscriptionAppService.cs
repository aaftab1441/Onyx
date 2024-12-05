using System.Threading.Tasks;
using Abp.Application.Services;

namespace Sixoclock.Onyx.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
