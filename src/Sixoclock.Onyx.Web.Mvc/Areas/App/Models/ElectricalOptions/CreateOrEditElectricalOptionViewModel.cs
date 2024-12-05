using Abp.AutoMapper;
using Sixoclock.Onyx.ElectricalOptions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ElectricalOptions
{
    [AutoMapFrom(typeof(GetElectricalOptionForEditOutput))]
    public class CreateOrEditElectricalOptionViewModel : GetElectricalOptionForEditOutput
    {
        public CreateOrEditElectricalOptionViewModel(GetElectricalOptionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
