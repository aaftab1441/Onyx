using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Markets.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Markets
{
    [AutoMapFrom(typeof(ListResultDto<MarketDto>))]
    public class MarketsViewModel : ListResultDto<MarketDto>
    {
        public MarketsViewModel(ListResultDto<MarketDto> output)
        {
            output.MapTo(this);
        }
    }
}
