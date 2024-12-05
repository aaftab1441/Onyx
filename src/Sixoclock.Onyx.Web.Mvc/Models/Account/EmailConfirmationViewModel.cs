using Sixoclock.Onyx.Authorization.Accounts.Dto;

namespace Sixoclock.Onyx.Web.Models.Account
{
    public class EmailConfirmationViewModel : ActivateEmailInput
    {
        /// <summary>
        /// Tenant id.
        /// </summary>
        public int? TenantId { get; set; }
    }
}