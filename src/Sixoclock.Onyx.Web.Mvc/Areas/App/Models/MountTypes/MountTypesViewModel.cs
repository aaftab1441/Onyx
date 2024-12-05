using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.MountTypes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.MountTypes
{
    [AutoMapFrom(typeof(ListResultDto<MountTypeDto>))]
    public class MountTypesViewModel : ListResultDto<MountTypeDto>
    {
        public MountTypesViewModel(ListResultDto<MountTypeDto> output)
        {
            output.MapTo(this);
        }
    }
}
