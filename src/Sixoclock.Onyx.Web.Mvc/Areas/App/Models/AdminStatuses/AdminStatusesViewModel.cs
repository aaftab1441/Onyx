using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.AdminStatuses.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.AdminStatuses
{
    [AutoMapFrom(typeof(ListResultDto<AdminStatusDto>))]
    public class AdminStatusesViewModel : ListResultDto<AdminStatusDto>
    {
        public AdminStatusesViewModel(ListResultDto<AdminStatusDto> output)
        {
            output.MapTo(this);
        }
    }
}
