using System.ComponentModel.DataAnnotations;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
