using Abp.Application.Services;
using Sixoclock.Onyx.KeyValues.Dto;

namespace Sixoclock.Onyx.KeyValues
{
    public interface IKeyValueAppService : IApplicationService
    {
        GetKeyValuesListByOCPPFeatureIdListOutput GetKeyValuesListByOCPPFeatureId(GetKeyValuesListByOCPPFeatureIdListInput input);
    }
}