using Abp.AutoMapper;
using Sixoclock.Onyx.Authorization.Roles.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Common;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode
        {
            get { return Role.Id.HasValue; }
        }

        public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}