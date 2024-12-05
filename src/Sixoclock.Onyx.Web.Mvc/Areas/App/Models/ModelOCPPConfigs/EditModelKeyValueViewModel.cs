using Abp.AutoMapper;
using Sixoclock.Onyx.ModelKeyValues.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelOCPPConfigs
{
    [AutoMapFrom(typeof(GetModelKeyValueForEditOutput))]
    public class EditModelKeyValueViewModel : GetModelKeyValueForEditOutput
    {
        public EditModelKeyValueViewModel(GetModelKeyValueForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
