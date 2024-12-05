using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.ElectricalOptions;
using Sixoclock.Onyx.ElectricalOptions.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Web.Areas.App.Models.ElectricalOptions;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class ElectricalOptionsController : OnyxControllerBase
    {
        private readonly IElectricalOptionAppService _electricalOptionAppService;

        public ElectricalOptionsController(IElectricalOptionAppService _electricalOptionAppService)
        {
            this._electricalOptionAppService = _electricalOptionAppService;
        }
        public async Task<ActionResult> Index(GetElectricalOptionInput input)
        {
            var output = await _electricalOptionAppService.GetElectricalOption(input);
            var model = new ElectricalOptionsViewModel(output);

            return View(model);
        }
        public async Task<PartialViewResult> CreateOrEditElectricalOptionModal(EntityDto<int> input)
        {
            var output = await _electricalOptionAppService.GetElectricalOptionForEdit(input);
            var viewModel = new CreateOrEditElectricalOptionViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}