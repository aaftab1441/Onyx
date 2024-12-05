using Abp.Application.Services;
using Sixoclock.Onyx.Units.Dto;

namespace Sixoclock.Onyx.Units
{
    public interface IUnitAppService : IApplicationService
    {
        GetUnitsListOutput GetUnitsList();
    }
}