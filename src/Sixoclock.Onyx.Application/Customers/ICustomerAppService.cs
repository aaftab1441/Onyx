using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerDto>> GetCustomer(GetCustomerInput input);
        Task CreateCustomer(CreateCustomerInput input);
        Task<GetCustomerForEditOutput> GetCustomerForEdit(EntityDto<int> input);
        Task<IEnumerable<CustomerListDto>> GetCustomersList();
    }
}