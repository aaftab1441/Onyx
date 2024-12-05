using Abp.AutoMapper;
using Sixoclock.Onyx.Connectors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Connectors
{
    [AutoMapFrom(typeof(GetConnectorForEditOutput))]
    public class CreateOrEditConnectorViewModel : GetConnectorForEditOutput
    {
        public CreateOrEditConnectorViewModel(GetConnectorForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
