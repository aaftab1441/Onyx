using Abp.AutoMapper;
using Sixoclock.Onyx.Installs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Installs
{
    [AutoMapFrom(typeof(GetInstallForEditOutput))]
    public class CreateOrEditInstallViewModel : GetInstallForEditOutput
    {
        public CreateOrEditInstallViewModel(GetInstallForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
