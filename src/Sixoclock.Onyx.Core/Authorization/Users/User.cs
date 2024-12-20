﻿using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Timing;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.Authorization.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    /// 
    [Ruleable(typeof(User))]
    public class User : AbpUser<User>
    {
        public const int MaxPhoneNumberLength = 24;

        public virtual Guid? ProfilePictureId { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public DateTime? SignInTokenExpireTimeUtc { get; set; }

        public string SignInToken { get; set; }

        public string GoogleAuthenticatorKey { get; set; }

        //Can add application specific user properties here
        [Ruleable(typeof(string))]
        public string Address { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        [Ruleable(typeof(string))]
        public string City { get; set; }
        [Ruleable(typeof(float))]
        public float? CostkWh { get; set; }
        [Ruleable(typeof(float))]
        public float? CostMin { get; set; }
        public float? TotalkWh { get; set; }
        public int? TotalSessions { get; set; }
        public int TotalChargeTime { get; set; }
        public int TotalCo2Saved { get; set; }
        [Ruleable(typeof(string))]
        public string Comment { get; set; }
        public int? CurrencyId { get; set; }
        public int? BillingTypeId { get; set; }
        public int? CountryId { get; set; }
        [Ruleable(typeof(Currency))]
        public virtual Currency Currency { get; set; }
        [Ruleable(typeof(Country))]
        public virtual Country Country { get; set; }
        public virtual BillingType BillingType { get; set; }
        public ICollection<UserRuleSet> UserRuleSets { get; set; }
        [Ruleable(typeof(string),"Email Address")]
        public override string EmailAddress { get; set; }

        public User()
        {
            IsLockoutEnabled = true;
            IsTwoFactorEnabled = true;
        }

        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public override void SetNewPasswordResetCode()
        {
            /* This reset code is intentionally kept short.
             * It should be short and easy to enter in a mobile application, where user can not click a link.
             */
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(10).ToUpperInvariant();
        }

        public void Unlock()
        {
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
        }

        public void SetSignInToken()
        {
            SignInToken = Guid.NewGuid().ToString();
            SignInTokenExpireTimeUtc = Clock.Now.AddMinutes(1).ToUniversalTime();
        }
    }
}