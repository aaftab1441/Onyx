using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Sixoclock.Onyx.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sixoclock.Onyx
{
    public class CustomerPolicy : OnyxServiceBase, ICustomerPolicy
    {
        private readonly IFeatureChecker _featureChecker;
        private readonly IRepository<Customer> _customerRepository;
        public CustomerPolicy(IFeatureChecker featureChecker, IRepository<Customer> customerRepository)
        {
            _featureChecker = featureChecker;
            _customerRepository = customerRepository;
        }
        public async Task CheckMaxCustomerCountAsync(int tenantId)
        {
            var maxcustomerCount = (await _featureChecker.GetValueAsync(tenantId, AppFeatures.MaxCustomerCount)).To<int>();
            if (maxcustomerCount <= 0)
            {
                return;
            }

            var currentCustomerCount = await _customerRepository.CountAsync();
            if (currentCustomerCount >= maxcustomerCount)
            {
                throw new UserFriendlyException(L("MaximumCustomerCount_Error_Message"), L("MaximumCustomerCount_Error_Detail", maxcustomerCount));
            }
        }
    }
}
