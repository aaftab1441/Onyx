using Abp.AutoMapper;
using Sixoclock.Onyx.AdminStatuses.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.AdminStatuses
{
    [AutoMapFrom(typeof(GetAdminStatusForEditOutput))]
    public class CreateOrEditAdminStatusViewModel : GetAdminStatusForEditOutput
    {
        public CreateOrEditAdminStatusViewModel(GetAdminStatusForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
