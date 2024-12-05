using Abp.AutoMapper;
using Sixoclock.Onyx.Configs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Configs
{
    [AutoMapFrom(typeof(GetConfigForEditOutput))]
    public class CreateOrEditConfigViewModel : GetConfigForEditOutput
    {
        public CreateOrEditConfigViewModel(GetConfigForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
