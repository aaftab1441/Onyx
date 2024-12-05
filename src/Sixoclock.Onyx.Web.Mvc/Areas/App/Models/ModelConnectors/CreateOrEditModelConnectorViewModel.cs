using Abp.AutoMapper;
using Sixoclock.Onyx.ModelConnectors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelConnectors
{
    [AutoMapFrom(typeof(GetModelConnectorForEditOutput))]
    public class CreateOrEditModelConnectorViewModel : GetModelConnectorForEditOutput
    {
        public CreateOrEditModelConnectorViewModel(GetModelConnectorForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
