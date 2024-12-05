using Sixoclock.Onyx.Sessions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Layout
{
    public class FooterViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetProductNameWithEdition()
        {
            const string productName = "Onyx";

            if (LoginInformations.Tenant == null)
            {
                return productName;
            }

            if (LoginInformations.Tenant.Edition == null)
            {
                return productName;
            }

            if (LoginInformations.Tenant.Edition.DisplayName == null)
            {
                return productName;
            }

            return productName + " " + LoginInformations.Tenant.Edition.DisplayName;
        }
    }
}