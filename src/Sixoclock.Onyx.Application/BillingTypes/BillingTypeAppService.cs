using Abp.Domain.Repositories;
using Sixoclock.Onyx.BillingTypes.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.BillingTypes
{
    public class BillingTypeAppService : OnyxAppServiceBase, IBillingTypeAppService
    {
        private readonly IRepository<BillingType> _billingTypeRepository;
        public BillingTypeAppService(IRepository<BillingType> billingTypeRepository)
        {
            _billingTypeRepository = billingTypeRepository;
        }

        public GetBillingTypesListOutput GetBillingTypesList()
        {
            IEnumerable<BillingTypeListDto> _billingTypesList = from billingType in _billingTypeRepository.GetAll()
                                                                          select new BillingTypeListDto
                                                                          {
                                                                              Id = billingType.Id,
                                                                              Name = billingType.Type
                                                                          };
            return new GetBillingTypesListOutput { BillingTypes = _billingTypesList.ToList() };
        }
    }
}
