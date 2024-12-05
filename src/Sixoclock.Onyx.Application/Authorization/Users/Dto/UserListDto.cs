using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Address { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        public float CostkWh { get; set; }

        public float CostMin { get; set; }

        public float TotalkWh { get; set; }

        public int TotalSessions { get; set; }

        public string Comment { get; set; }

        public int? CurrencyId { get; set; }

        public int? BillingTypeId { get; set; }

        public int? CountryId { get; set; }


        public List<UserListRoleDto> Roles { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        [AutoMapFrom(typeof(UserRole))]
        public class UserListRoleDto
        {
            public int RoleId { get; set; }

            public string RoleName { get; set; }
        }
    }
}