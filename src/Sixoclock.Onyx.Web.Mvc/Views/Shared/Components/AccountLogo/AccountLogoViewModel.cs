using Sixoclock.Onyx.Sessions.Dto;

namespace Sixoclock.Onyx.Web.Views.Shared.Components.AccountLogo
{
    public class AccountLogoViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; }

        public AccountLogoViewModel(GetCurrentLoginInformationsOutput loginInformations)
        {
            LoginInformations = loginInformations;
        }

        public string GetLogoUrl(string appPath)
        {
            if (LoginInformations.Tenant?.LogoId == null)
            {
                return appPath + "Common/Images/spine3.png";
            }

            return appPath + "TenantCustomization/GetLogo?id=" + LoginInformations.Tenant.LogoId;
        }
    }
}