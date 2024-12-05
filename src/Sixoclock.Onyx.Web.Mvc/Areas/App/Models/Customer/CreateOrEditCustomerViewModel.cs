using Abp.AutoMapper;
using Sixoclock.Onyx.Security;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Customer
{
    [AutoMapFrom(typeof(GetCustomerForEditOutput))]
    public class CreateOrEditCustomerViewModel : GetCustomerForEditOutput
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }
        public CreateOrEditCustomerViewModel(GetCustomerForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
