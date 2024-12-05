using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Installs
{
    [AutoMapFrom(typeof(GetInstallServiceForEditOutput))]
    public class CreateOrEditInstallServiceViewModel : GetInstallServiceForEditOutput
    {
        public CreateOrEditInstallServiceViewModel(GetInstallServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }
   
}
