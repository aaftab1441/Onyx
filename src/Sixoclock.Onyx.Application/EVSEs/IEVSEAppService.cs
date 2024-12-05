using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.EVSEs.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.EVSEs
{
    public interface IEVSEAppService : IApplicationService
    {
        Task CreateOrUpdateEVSE(CreateOrUpdateEVSEInput input);
        Task DeleteEVSE(EntityDto<int> input);
        Task<PagedResultDto<EVSEDto>> GetEVSE(GetEVSEInput input);
        Task<ListResultDto<EVSEDto>> GetEVSEByChargepointId(EntityDto<int> input);
        GetEVSEForEditOutput GetEVSEForEdit(EntityDto<int> input);
        Task<GetEVSEsListOutput> GetEVSEsList();
        Task<GetEVSEsListOutput> GetEVSEsListByChargepoint(EntityDto<int> input);
        Task CopyModelEVSEsAndConnectorsToEVSEsAndConnectors(int chargepointModelId, int chargepointId);
    }
}