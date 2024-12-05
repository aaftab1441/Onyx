using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ModelEVSEs.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ModelEVSEs
{
    public interface IModelEVSEAppService : IApplicationService
    {
        Task CreateOrUpdateModelEVSE(CreateOrUpdateModelEVSEInput input);
        Task DeleteModelEVSE(EntityDto<int> input);
        Task<PagedResultDto<ModelEVSEDto>> GetModelEVSE(GetModelEVSEInput input);
        GetModelEVSEForEditOutput GetModelEVSEForEdit(EntityDto<int> input);
        GetModelEVSEsListOutput GetModelEVSEsList();
        GetModelEVSEsListOutput GetModelEVSEsList(int chargepointModelId);
    }
}