using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.MultiTenancy.Accounting;
using Sixoclock.Onyx.Web.Areas.App.Models.Accounting;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    public class InvoiceController : OnyxControllerBase
    {
        private readonly IInvoiceAppService _invoiceAppService;

        public InvoiceController(IInvoiceAppService invoiceAppService)
        {
            _invoiceAppService = invoiceAppService;
        }


        [HttpGet]
        public async Task<ActionResult> Index(long paymentId)
        {
            var invoice = await _invoiceAppService.GetInvoiceInfo(new EntityDto<long>(paymentId));
            var model = new InvoiceViewModel
            {
                Invoice = invoice
            };

            return View(model);
        }
    }
}