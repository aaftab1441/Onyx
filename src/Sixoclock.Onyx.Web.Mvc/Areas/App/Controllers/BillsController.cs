using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Bills;
using Sixoclock.Onyx.Bills.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.Bills;
using Sixoclock.Onyx.Web.Areas.App.Models.Connectors;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Bills)]
    public class BillsController:OnyxControllerBase
    {
        private readonly IBillAppService _billAppService;

        public BillsController(IBillAppService billAppService)
        {
            _billAppService = billAppService;
        }

        public ActionResult Index(GetBillInput input)
        {
           
            return View();
        }

        public ActionResult GetBillToAddComment(AddCommentInputDto input)
        {
            return PartialView("_AddCommentModal",new AddCommentModalViewModel(input));
        }

    }
}
