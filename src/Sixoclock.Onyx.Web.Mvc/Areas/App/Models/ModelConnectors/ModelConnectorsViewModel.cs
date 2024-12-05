using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ModelConnectors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelConnectors
{
    [AutoMapFrom(typeof(ListResultDto<ModelConnectorDto>))]
    public class ModelConnectorsViewModel : ListResultDto<ModelConnectorDto>
    {
        public ModelConnectorsViewModel(ListResultDto<ModelConnectorDto> output)
        {
            output.MapTo(this);
        }
    }
}
