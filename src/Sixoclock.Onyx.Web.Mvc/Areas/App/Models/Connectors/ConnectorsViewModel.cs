using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Connectors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Connectors
{
    [AutoMapFrom(typeof(ListResultDto<ConnectorDto>))]
    public class ConnectorsViewModel : ListResultDto<ConnectorDto>
    {
        public ConnectorsViewModel(ListResultDto<ConnectorDto> output)
        {
            output.MapTo(this);
        }
    }
}
