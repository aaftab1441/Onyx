using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.ModelEVSEs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelEVSEs
{
    [AutoMapFrom(typeof(ListResultDto<ModelEVSEDto>))]
    public class ModelEVSEsViewModel : ListResultDto<ModelEVSEDto>
    {
        public ModelEVSEsViewModel(ListResultDto<ModelEVSEDto> output)
        {
            output.MapTo(this);
        }
    }
}
