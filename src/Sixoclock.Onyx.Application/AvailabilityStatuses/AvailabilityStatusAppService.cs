using Abp.Domain.Repositories;
using Sixoclock.Onyx.AvailabilityStatuses.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.AvailabilityStatuses
{
    public class AvailabilityStatusAppService : OnyxAppServiceBase, IAvailabilityStatusAppService
    {
        private readonly IRepository<AvailabilityStatus> _availabilityStatusRepository;
        public AvailabilityStatusAppService(IRepository<AvailabilityStatus> availabilityStatusRepository)
        {
            _availabilityStatusRepository = availabilityStatusRepository;
        }

        public GetAvailabilityStatusesListOutput GetAvailabilityStatussList()
        {
            IEnumerable<AvailabilityStatusListDto> _availabilityStatussList = from availabilityStatus in _availabilityStatusRepository.GetAll()
                                                                              select new AvailabilityStatusListDto
                                                                              {
                                                                                  Id = availabilityStatus.Id,
                                                                                  Name = availabilityStatus.Value
                                                                              };
            return new GetAvailabilityStatusesListOutput { AvailabilityStatuses = _availabilityStatussList.ToList() };
        }
        public int GetAvailabilityStatus(string input)
        {
            var availabilityStatuses = from availabilityStatus in _availabilityStatusRepository.GetAll().Where(r => r.Value.Contains(input))
                                       select availabilityStatus;

            return availabilityStatuses.FirstOrDefault().Id;
        }
    }
}
