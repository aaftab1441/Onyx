using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Markets
{
    [AutoMapFrom(typeof(GetMarketServiceForEditOutput))]
    public class CreateOrEditMarketServiceViewModel : GetMarketServiceForEditOutput
    {
        public CreateOrEditMarketServiceViewModel(GetMarketServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
