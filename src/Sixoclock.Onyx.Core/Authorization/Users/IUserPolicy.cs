using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Sixoclock.Onyx.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
