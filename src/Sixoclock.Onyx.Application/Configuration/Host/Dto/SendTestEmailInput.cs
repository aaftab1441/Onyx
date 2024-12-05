using System.ComponentModel.DataAnnotations;
using Sixoclock.Onyx.Authorization.Users;

namespace Sixoclock.Onyx.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}