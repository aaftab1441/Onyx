using System.ComponentModel.DataAnnotations;

namespace Sixoclock.Onyx.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}