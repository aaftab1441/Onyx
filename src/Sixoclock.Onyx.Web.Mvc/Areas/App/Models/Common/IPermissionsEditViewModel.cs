using System.Collections.Generic;
using Sixoclock.Onyx.Authorization.Permissions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}