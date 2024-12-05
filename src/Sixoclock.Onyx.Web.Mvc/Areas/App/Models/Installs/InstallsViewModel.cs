using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Installs.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Installs
{
    [AutoMapFrom(typeof(ListResultDto<InstallDto>))]
    public class InstallsViewModel : ListResultDto<InstallDto>
    {
        public InstallsViewModel(ListResultDto<InstallDto> output)
        {
            output.MapTo(this);
        }
    }
}
