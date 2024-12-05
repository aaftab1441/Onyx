using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.OtherOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.OtherOptions
{
    [AutoMapFrom(typeof(ListResultDto<OtherOptionDto>))]
    public class OtherOptionsViewModel : ListResultDto<OtherOptionDto>
    {
        public OtherOptionsViewModel(ListResultDto<OtherOptionDto> output)
        {
            output.MapTo(this);
        }
    }
}
