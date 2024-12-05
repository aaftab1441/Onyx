using Abp.AutoMapper;
using Sixoclock.Onyx.ModelEVSEs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.ModelEVSEs
{
    [AutoMapFrom(typeof(GetModelEVSEForEditOutput))]
    public class CreateOrEditModelEVSEViewModel : GetModelEVSEForEditOutput
    {
        public CreateOrEditModelEVSEViewModel(GetModelEVSEForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
