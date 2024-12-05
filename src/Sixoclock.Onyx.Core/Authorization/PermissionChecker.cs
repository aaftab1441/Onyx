using Abp.Authorization;
using Sixoclock.Onyx.Authorization.Roles;
using Sixoclock.Onyx.Authorization.Users;

namespace Sixoclock.Onyx.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
