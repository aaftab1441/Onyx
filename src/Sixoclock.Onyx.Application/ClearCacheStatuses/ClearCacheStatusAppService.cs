using Abp.Domain.Repositories;
using Sixoclock.Onyx.ClearCacheStatuses.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.ClearCacheStatuses
{
    public class ClearCacheStatusAppService : OnyxAppServiceBase, IClearCacheStatusAppService
    {
        private readonly IRepository<ClearCacheStatus> _clearCacheStatusRepository;
        public ClearCacheStatusAppService(IRepository<ClearCacheStatus> clearCacheStatusRepository)
        {
            _clearCacheStatusRepository = clearCacheStatusRepository;
        }

        public GetClearCacheStatusesListOutput GetClearCacheStatussList()
        {
            IEnumerable<ClearCacheStatusListDto> _clearCacheStatussList = from clearCacheStatus in _clearCacheStatusRepository.GetAll()
                                                                          select new ClearCacheStatusListDto
                                                                          {
                                                                              Id = clearCacheStatus.Id,
                                                                              Name = clearCacheStatus.Value
                                                                          };
            return new GetClearCacheStatusesListOutput { ClearCacheStatuses = _clearCacheStatussList.ToList() };
        }
        public int GetClearCacheStatus(string input)
        {
            var clearCacheStatuses = from clearCacheStatus in _clearCacheStatusRepository.GetAll().Where(r => r.Value.Contains(input))
                                     select clearCacheStatus;

            return clearCacheStatuses.FirstOrDefault().Id;
        }
    }
}
