using Abp.Domain.Policies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sixoclock.Onyx
{
    public interface ICustomerPolicy:IPolicy
    {
        Task CheckMaxCustomerCountAsync(int tenantId);
    }
}
