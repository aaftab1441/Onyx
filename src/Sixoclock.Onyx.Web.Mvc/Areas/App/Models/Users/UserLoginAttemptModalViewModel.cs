using System.Collections.Generic;
using Sixoclock.Onyx.Authorization.Users.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}