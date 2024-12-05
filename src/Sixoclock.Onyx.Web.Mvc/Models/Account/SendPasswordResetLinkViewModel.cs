using System.ComponentModel.DataAnnotations;

namespace Sixoclock.Onyx.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}