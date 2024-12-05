using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ChargepointModelImages.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointModelImages
{
    [AutoMapFrom(typeof(ListResultDto<ChargepointModelImageDto>))]
    public class ChargepointModelImagesViewModel : ListResultDto<ChargepointModelImageDto>
    {
        public ChargepointModelImagesViewModel(ListResultDto<ChargepointModelImageDto> output)
        {
            output.MapTo(this);
        }
    }
}
