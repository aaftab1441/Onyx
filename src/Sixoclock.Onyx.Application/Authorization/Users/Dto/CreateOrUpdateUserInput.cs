using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class CreateOrUpdateUserInput
    {
        [Required]
        public UserEditDto User { get; set; }

        public TagEditDto Tag { get; set; }

        public BillingEditDto Billing { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }

        public List<long> OrganizationUnits { get; set; }

        public CreateOrUpdateUserInput()
        {
            OrganizationUnits = new List<long>();
        }
    }
}