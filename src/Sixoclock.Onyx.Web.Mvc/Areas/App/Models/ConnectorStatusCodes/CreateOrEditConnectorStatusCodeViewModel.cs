using Abp.AutoMapper;
using Sixoclock.Onyx.ConnectorStatusCodes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ConnectorStatusCodes
{
    [AutoMapFrom(typeof(GetConnectorStatusCodeForEditOutput))]
    public class CreateOrEditConnectorStatusCodeViewModel : GetConnectorStatusCodeForEditOutput
    {
        public CreateOrEditConnectorStatusCodeViewModel(GetConnectorStatusCodeForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
