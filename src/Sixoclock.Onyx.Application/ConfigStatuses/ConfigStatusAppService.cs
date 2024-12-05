using Abp.Domain.Repositories;
using Sixoclock.Onyx.ConfigStatuses.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.ConfigStatuses
{
    public class ConfigStatusAppService : OnyxAppServiceBase, IConfigStatusAppService
    {
        private readonly IRepository<ConfigStatus> _configStatusRepository;
        public ConfigStatusAppService(IRepository<ConfigStatus> configStatusRepository)
        {
            _configStatusRepository = configStatusRepository;
        }

        public GetConfigStatusesListOutput GetConfigStatussList()
        {
            IEnumerable<ConfigStatusListDto> _configStatussList = from configStatus in _configStatusRepository.GetAll()
                                                                  select new ConfigStatusListDto
                                                                  {
                                                                      Id = configStatus.Id,
                                                                      Name = configStatus.Value
                                                                  };
            return new GetConfigStatusesListOutput { ConfigStatuses = _configStatussList.ToList() };
        }
        public int GetConfigStatus(string input)
        {
            var configStatuses = from configStatus in _configStatusRepository.GetAll().Where(r => r.Value.Contains(input))
                                 select configStatus;

            return configStatuses.FirstOrDefault().Id;
        }
    }
}
