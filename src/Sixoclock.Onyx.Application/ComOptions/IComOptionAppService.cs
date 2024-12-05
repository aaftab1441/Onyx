using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ComOptions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ComOptions
{
    public interface IComOptionAppService : IApplicationService
    {
        Task CreateOrUpdateComOption(CreateOrUpdateComOptionInput input);
        Task DeleteComOption(EntityDto<int> input);
        Task<PagedResultDto<ComOptionDto>> GetComOption(GetComOptionInput input);
        Task<GetComOptionForEditOutput> GetComOptionForEdit(EntityDto<int> input);
    }
}