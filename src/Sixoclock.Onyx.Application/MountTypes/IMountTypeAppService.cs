using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.MountTypes.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.MountTypes
{
    public interface IMountTypeAppService : IApplicationService
    {
        Task CreateOrUpdateMountType(CreateOrUpdateMountTypeInput input);
        Task DeleteMountType(EntityDto<int> input);
        Task<PagedResultDto<MountTypeDto>> GetMountType(GetMountTypeInput input);
        Task<GetMountTypeForEditOutput> GetMountTypeForEdit(EntityDto<int> input);
        GetMountTypesListOutput GetMountTypesList();
    }
}