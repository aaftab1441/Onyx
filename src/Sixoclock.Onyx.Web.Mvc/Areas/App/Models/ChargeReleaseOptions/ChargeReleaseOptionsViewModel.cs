using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ChargeReleaseOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargeReleaseOptions
{
    [AutoMapFrom(typeof(ListResultDto<ChargeReleaseOptionDto>))]
    public class ChargeReleaseOptionsViewModel : ListResultDto<ChargeReleaseOptionDto>
    {
        public ChargeReleaseOptionsViewModel(ListResultDto<ChargeReleaseOptionDto> output)
        {
            output.MapTo(this);
        }
    }
}
