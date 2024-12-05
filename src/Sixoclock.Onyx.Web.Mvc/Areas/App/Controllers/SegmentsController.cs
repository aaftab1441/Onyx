using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Sixoclock.Onyx.Web.Controllers;
using Sixoclock.Onyx.Segments;
using Sixoclock.Onyx.Segments.Dto;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Services;
using Sixoclock.Onyx.Web.Areas.App.Models.Segments;
using Sixoclock.Onyx.TagTransactions;
using Sixoclock.Onyx.Web.Areas.App.Models.Services;
using Sixoclock.Onyx.Web.Areas.App.Models.TagTransactions;

namespace Sixoclock.Onyx.Web.Mvc.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class SegmentsController : OnyxControllerBase
    {
        private readonly ISegmentAppService _segmentAppService;
        private readonly ITagTransactionAppService _tagTransactionAppService;
        private readonly IServicesAppService _servicesAppService;
        private readonly ISegmentServiceAppService _segmentServiceAppService;
        private readonly ISegmentServicePriceParameterAppService _segmentServicePriceParameterAppService;

        public SegmentsController(ISegmentAppService segmentAppService,
            ITagTransactionAppService tagTransactionAppService, 
            IServicesAppService servicesAppService,
            ISegmentServiceAppService segmentServiceAppService, ISegmentServicePriceParameterAppService segmentServicePriceParameterAppService)
        {
            _segmentAppService = segmentAppService;
            _tagTransactionAppService = tagTransactionAppService;
            _servicesAppService = servicesAppService;
            _segmentServiceAppService = segmentServiceAppService;
            _segmentServicePriceParameterAppService = segmentServicePriceParameterAppService;
        }

        public async Task<ActionResult> Index(GetSegmentInput input)
        {
            var output = await _segmentAppService.GetSegment(input);
            var model = new SegmentsViewModel(output);

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditSegmentModal(EntityDto<int> input)
        {
          
            var output = await _segmentAppService.GetSegmentForEdit(input);
            var viewModel = new CreateOrEditSegmentViewModel(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult SegmentDashboardModal(EntityDto<int> input)
        {
            var output = _tagTransactionAppService.GetTransactionsDashboardBySegment(input);
            var viewModel = new DashboardViewModel(output);
            ViewBag.DashBoard = "Segment : Private";
            return PartialView("../TopologyDashboard/_Dashboard", viewModel);
        }
        public PartialViewResult SegmentUtilisationModal(EntityDto<int> input)
        {
            ViewBag.Utilisation = "Segment : Private";
            ViewBag.Id = input.Id;
            return PartialView("../TopologyUtilisation/_Utilisation");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult SegmentServiceModel(int id)
        {
            ViewBag.SegmentId = id;
            return PartialView("_SegmentService");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditSegmentServiceModal(string id,int segmentId)
        {
            var output = await _segmentServiceAppService.GetSegmentServiceForEdit(
                new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id) });
            var viewModel = new CreateOrEditSegmentServiceViewModel(output);
            ViewBag.SegmentId = segmentId;
            return PartialView("_CreateOrEditSegmentServiceModal", viewModel);

        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services)]
        public PartialViewResult SegmentServicePriceParameterModel(int id)
        {
            ViewBag.SegmentServiceId = id;
            return PartialView("_PriceParameters");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Services_Create, AppPermissions.Pages_Services_Edit)]
        public async Task<PartialViewResult> CreateOrEditSegmentServicePriceParameterModal(string id,string serviceId)
        {
            var output = await _segmentServicePriceParameterAppService.GeServicePriceParameterForEdit(new Services.Dto.GetServicePriceParametersForEditParamInput<int> { Id = Convert.ToInt32(id), ServiceId = Convert.ToInt32(serviceId) });
            var viewModel = new CreateOrEditServicesPriceParameterModalViewModel(output);

            return PartialView("CreateOrEditSegmentServicePriceParameterModal", viewModel);

        }
    }
}