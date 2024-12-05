using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.MultiTenancy.Accounting.Dto;

namespace Sixoclock.Onyx.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
