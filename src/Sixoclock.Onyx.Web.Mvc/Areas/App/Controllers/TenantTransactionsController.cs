using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Transactions;
using Sixoclock.Onyx.Transactions.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Transactions;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class TenantTransactionsController : OnyxControllerBase
    {
        private readonly ITransactionAppService _transactionAppService;
        
        public TenantTransactionsController(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }
        public async Task<ActionResult> Index(GetTransactionInput input)
        {
            var output = await _transactionAppService.GetTenantTransactions(input);
            var model = new TransactionsViewModel(output);

            return View(model);
        }
    }
}