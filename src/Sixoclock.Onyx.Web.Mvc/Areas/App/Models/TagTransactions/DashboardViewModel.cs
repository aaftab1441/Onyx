using Abp.AutoMapper;
using Sixoclock.Onyx.TagTransactions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions
{
    [AutoMapFrom(typeof(GetTransactionsOverviewOutput))]
    public class DashboardViewModel : GetTransactionsOverviewOutput
    {
        public DashboardViewModel(GetTransactionsOverviewOutput output)
        {
            output.MapTo(this);
        }
    }
}
