using Abp.Domain.Repositories;
using Sixoclock.Onyx.UnlockStatuses.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.UnlockStatuses
{
    public class UnlockStatusAppService : OnyxAppServiceBase, IUnlockStatusAppService
    {
        private readonly IRepository<UnlockStatus> _unlockStatusRepository;
        public UnlockStatusAppService(IRepository<UnlockStatus> unlockStatusRepository)
        {
            _unlockStatusRepository = unlockStatusRepository;
        }

        public GetUnlockStatusesListOutput GetUnlockStatussList()
        {
            IEnumerable<UnlockStatusListDto> _unlockStatussList = from unlockStatus in _unlockStatusRepository.GetAll()
                                                                  select new UnlockStatusListDto
                                                                  {
                                                                      Id = unlockStatus.Id,
                                                                      Name = unlockStatus.Value
                                                                  };
            return new GetUnlockStatusesListOutput { UnlockStatuses = _unlockStatussList.ToList() };
        }
        public int GetUnlockStatus(string input)
        {
            var unlockStatuses = from unlockStatus in _unlockStatusRepository.GetAll().Where(r => r.Value.Contains(input))
                                 select unlockStatus;

            return unlockStatuses.FirstOrDefault().Id;
        }
    }
}
