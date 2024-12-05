using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.ConnectorStatusCodes;
using Sixoclock.Onyx.ConnectorStatusCodes.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ConnectorStatusCodes;
using Sixoclock.Onyx.Web.Controllers;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ConnectorStatusCodesController : OnyxControllerBase
    {
        private readonly IConnectorStatusCodeAppService _connectorStatusCodeAppService;

        public ConnectorStatusCodesController(IConnectorStatusCodeAppService connectorStatusCodeAppService)
        {
            _connectorStatusCodeAppService = connectorStatusCodeAppService;
        }
        public async Task<ActionResult> Index(GetConnectorStatusCodeInput input)
        {
            var output = await _connectorStatusCodeAppService.GetConnectorStatusCode(input);
            var model = new ConnectorStatusCodesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditConnectorStatusCodeModal(EntityDto<int> input)
        {
            var output = await _connectorStatusCodeAppService.GetConnectorStatusCodeForEdit(input);
            var viewModel = new CreateOrEditConnectorStatusCodeViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}