using System.Threading.Tasks;
using Sixoclock.Onyx.Security.Recaptcha;

namespace Sixoclock.Onyx.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
