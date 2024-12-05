using System.Threading.Tasks;
using Abp.Dependency;

namespace Sixoclock.Onyx.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}