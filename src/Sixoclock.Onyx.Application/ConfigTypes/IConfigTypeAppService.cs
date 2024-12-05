using Abp.Application.Services;
using Sixoclock.Onyx.ConfigTypes.Dto;

namespace Sixoclock.Onyx.ConfigTypes
{
    public interface IConfigTypeAppService : IApplicationService
    {
        int GetConfigType(string input);
        GetConfigTypesListOutput GetConfigTypesList();
    }
}