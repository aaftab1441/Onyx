using Abp.AutoMapper;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Customer
{
    [AutoMapFrom(typeof(GetCustomerServiceForEditOutput))]
    public class CreateOrEditCustomerServiceViewModel : GetCustomerServiceForEditOutput
    {
        public CreateOrEditCustomerServiceViewModel(GetCustomerServiceForEditOutput output)
        {
            output.MapTo(this);
        }
    }

}
