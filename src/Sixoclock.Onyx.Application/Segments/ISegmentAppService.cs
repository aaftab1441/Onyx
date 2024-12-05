using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Segments.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Segments
{
    public interface ISegmentAppService : IApplicationService
    {
        Task CreateOrUpdateSegment(CreateOrUpdateSegmentInput input);
        Task DeleteSegment(EntityDto<int> input);
        Task<PagedResultDto<segmentListDtos>> GetSegment(GetSegmentInput input);
        Task<GetSegmentForEditOutput> GetSegmentForEdit(EntityDto<int> input);
        Task<GetSegmentsListOutput> GetSegmentsList();
    }
}