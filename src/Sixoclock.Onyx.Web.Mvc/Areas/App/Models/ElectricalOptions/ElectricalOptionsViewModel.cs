using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ElectricalOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ElectricalOptions
{
    [AutoMapFrom(typeof(ListResultDto<ElectricalOptionDto>))]
    public class ElectricalOptionsViewModel : ListResultDto<ElectricalOptionDto>
    {
        public ElectricalOptionsViewModel(ListResultDto<ElectricalOptionDto> output)
        {
            output.MapTo(this);
        }
    }
}
