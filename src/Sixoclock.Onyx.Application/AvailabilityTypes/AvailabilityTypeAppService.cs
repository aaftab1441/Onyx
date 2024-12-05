using Abp.Domain.Repositories;
using Sixoclock.Onyx.AvailabilityTypes.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.AvailabilityTypes
{
    public class AvailabilityTypeAppService : OnyxAppServiceBase, IAvailabilityTypeAppService
    {
        private readonly IRepository<AvailabilityType> _availabilityTypeRepository;
        public AvailabilityTypeAppService(IRepository<AvailabilityType> availabilityTypeRepository)
        {
            _availabilityTypeRepository = availabilityTypeRepository;
        }

        public GetAvailabilityTypesListOutput GetAvailabilityTypesList()
        {
            IEnumerable<AvailabilityTypeListDto> _availabilityTypesList = from availabilityType in _availabilityTypeRepository.GetAll()
                                                                          select new AvailabilityTypeListDto
                                                                          {
                                                                              Id = availabilityType.Id,
                                                                              Name = availabilityType.Value
                                                                          };
            return new GetAvailabilityTypesListOutput { AvailabilityTypes = _availabilityTypesList.ToList() };
        }
    }
}
