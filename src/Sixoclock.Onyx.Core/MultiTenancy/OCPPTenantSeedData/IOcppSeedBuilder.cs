using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Sixoclock.Onyx.MultiTenancy.OCPPTenantSeedData
{
    public interface IOcppSeedBuilder:IDomainService
    {

        Task SeedOcppDataAsync(int tenantId);
    }
}
