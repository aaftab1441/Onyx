using System;
using System.Collections.Generic;
using Sixoclock.Onyx.Organizations.Dto;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class GetUserForEditOutput
    {
        public Guid? ProfilePictureId { get; set; }

        public UserEditDto User { get; set; }

        public TagEditDto Tag { get; set; }

        public BillingEditDto Billing { get; set; }

        public UserRoleDto[] Roles { get; set; }

        public List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        public List<string> MemberedOrganizationUnits { get; set; }
    }
}