using Abp.Domain.Repositories;
using Sixoclock.Onyx.KeyValues.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.KeyValues
{
    public class KeyValueAppService : OnyxAppServiceBase, IKeyValueAppService
    {
        private readonly IRepository<KeyValue> _keyValueRepository;
        public KeyValueAppService(IRepository<KeyValue> keyValueRepository)
        {
            _keyValueRepository = keyValueRepository;
        }
        public GetKeyValuesListByOCPPFeatureIdListOutput GetKeyValuesListByOCPPFeatureId(GetKeyValuesListByOCPPFeatureIdListInput input)
        {
            IEnumerable<KeyValueByOCPPFeatureIdListDto> _keyValuesList = from keyValue in _keyValueRepository.GetAll().Where(k=>k.TenantId == input.TenantId && k.OCPPFeatureId == input.OCPPFeatureId)
                                                      select new KeyValueByOCPPFeatureIdListDto
                                                      {
                                                          Id = keyValue.Id,
                                                          DefaultValue = keyValue.DefaultValue,
                                                          RW = keyValue.RW,
                                                          Comment = keyValue.Comment,
                                                          Key = keyValue.Key,
                                                          FeatureName = keyValue.OCPPFeature.FeatureName,
                                                          TenantId = keyValue.TenantId
                                                      };
            return new GetKeyValuesListByOCPPFeatureIdListOutput { KeyValues = _keyValuesList.ToList() };
        }
    }
}
