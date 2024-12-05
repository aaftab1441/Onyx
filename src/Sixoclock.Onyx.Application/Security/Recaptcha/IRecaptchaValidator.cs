using System.Threading.Tasks;

namespace Sixoclock.Onyx.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}