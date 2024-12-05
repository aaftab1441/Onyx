using Abp.AutoMapper;
using Sixoclock.Onyx.Capacities.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Capacities
{
    [AutoMapFrom(typeof(GetCapacityForEditOutput))]
    public class CreateOrEditCapacitiesViewModel : GetCapacityForEditOutput
    {
        public CreateOrEditCapacitiesViewModel(GetCapacityForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
