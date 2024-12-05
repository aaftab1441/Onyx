using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ConnectorStatusCodes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ConnectorStatusCodes
{
    [AutoMapFrom(typeof(ListResultDto<ConnectorStatusCodeDto>))]
    public class ConnectorStatusCodesViewModel : ListResultDto<ConnectorStatusCodeDto>
    {
        public ConnectorStatusCodesViewModel(ListResultDto<ConnectorStatusCodeDto> output)
        {
            output.MapTo(this);
        }
    }
}
