using Abp.AutoMapper;
using Sixoclock.Onyx.EVSEs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.EVSEs
{
    [AutoMapFrom(typeof(GetEVSEForEditOutput))]
    public class CreateOrEditEVSEViewModel : GetEVSEForEditOutput
    {
        public CreateOrEditEVSEViewModel(GetEVSEForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
