using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ChargepointKeyValues;
using Sixoclock.Onyx.ChargepointKeyValues.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ChargepointOCPPConfigs;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ChargepointOCPPConfigsController : OnyxControllerBase
    {
        private readonly IChargepointKeyValueAppService _chargepointKeyValueAppService;

        public ChargepointOCPPConfigsController(IChargepointKeyValueAppService chargepointKeyValueAppService)
        {
            _chargepointKeyValueAppService = chargepointKeyValueAppService;
        }
        public async Task<ActionResult> Index(GetChargepointKeyValueInput input)
        {
            var output = await _chargepointKeyValueAppService.GetChargepointKeyValue(input);
            var model = new ChargepointKeyValuesViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> EditChargepointKeyValueModal(EntityDto<int> input)
        {
            var output = await _chargepointKeyValueAppService.GetChargepointKeyValueForEdit(input);
            var viewModel = new EditChargepointKeyValueViewModel(output);

            return PartialView("_EditModal", viewModel);
        }
    }
}