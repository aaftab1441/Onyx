using Abp.AutoMapper;
using Sixoclock.Onyx.Markets.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Markets
{
    [AutoMapFrom(typeof(GetMarketForEditOutput))]
    public class CreateOrEditMarketViewModel : GetMarketForEditOutput
    {
        public CreateOrEditMarketViewModel(GetMarketForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
