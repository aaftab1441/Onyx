using Abp.Domain.Repositories;
using Sixoclock.Onyx.ConfigTypes.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.ConfigTypes
{
    public class ConfigTypeAppService : OnyxAppServiceBase, IConfigTypeAppService
    {
        private readonly IRepository<ConfigType> _configTypeRepository;
        public ConfigTypeAppService(IRepository<ConfigType> configTypeRepository)
        {
            _configTypeRepository = configTypeRepository;
        }

        public GetConfigTypesListOutput GetConfigTypesList()
        {
            IEnumerable<ConfigTypeListDto> _configTypesList = from configType in _configTypeRepository.GetAll()
                                                              select new ConfigTypeListDto
                                                              {
                                                                  Id = configType.Id,
                                                                  Name = configType.Type
                                                              };
            return new GetConfigTypesListOutput { ConfigTypes = _configTypesList.ToList() };
        }
        public int GetConfigType(string input)
        {
            var configTypes = from configType in _configTypeRepository.GetAll().Where(r => r.Type.Contains(input))
                              select configType;

            return configTypes.FirstOrDefault().Id;
        }
    }
}
