using Abp.AutoMapper;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ChargepointOCPPConfigs
{
    [AutoMapFrom(typeof(GetChargepointKeyValueForEditOutput))]
    public class EditChargepointKeyValueViewModel : GetChargepointKeyValueForEditOutput
    {
        public EditChargepointKeyValueViewModel(GetChargepointKeyValueForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
