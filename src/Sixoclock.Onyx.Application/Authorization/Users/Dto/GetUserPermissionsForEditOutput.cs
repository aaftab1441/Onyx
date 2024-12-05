using System.Collections.Generic;
using Sixoclock.Onyx.Authorization.Permissions.Dto;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}