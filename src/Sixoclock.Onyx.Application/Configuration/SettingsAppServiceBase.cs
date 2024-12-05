using System.Threading.Tasks;
using Abp.Net.Mail;
using Sixoclock.Onyx.Configuration.Host.Dto;

namespace Sixoclock.Onyx.Configuration
{
    public abstract class SettingsAppServiceBase : OnyxAppServiceBase
    {
        private readonly IEmailSender _emailSender;

        protected SettingsAppServiceBase(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        #region Send Test Email

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            await _emailSender.SendAsync(
                input.EmailAddress,
                L("TestEmail_Subject"),
                L("TestEmail_Body")
            );
        }

        #endregion
    }
}
