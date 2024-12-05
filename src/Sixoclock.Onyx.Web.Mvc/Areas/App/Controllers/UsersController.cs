using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Authorization.Permissions;
using Sixoclock.Onyx.Authorization.Roles;
using Sixoclock.Onyx.Authorization.Roles.Dto;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Security;
using Sixoclock.Onyx.Web.Areas.App.Models.Users;
using Sixoclock.Onyx.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sixoclock.Onyx.Countries;
using Sixoclock.Onyx.Currencies;
using Sixoclock.Onyx.BillingTypes;
using Sixoclock.Onyx.UserGrants;

namespace Sixoclock.Onyx.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : OnyxControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        private readonly IUserLoginAppService _userLoginAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;
        private readonly ICountryAppService _countryAppService;
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IBillingTypeAppService _billingTypeAppService;
        private readonly IUserRuleSetService _userRuleSetService;

        public UsersController(
            IUserAppService userAppService,
            UserManager userManager,
            IUserLoginAppService userLoginAppService,
            IRoleAppService roleAppService,
            IPermissionAppService permissionAppService,
            IPasswordComplexitySettingStore passwordComplexitySettingStore,
            ICountryAppService countryAppService,
            ICurrencyAppService currencyAppService,
            IBillingTypeAppService billingTypeAppService,
            IUserRuleSetService userRuleSetService)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            _userLoginAppService = userLoginAppService;
            _roleAppService = roleAppService;
            _permissionAppService = permissionAppService;
            _passwordComplexitySettingStore = passwordComplexitySettingStore;
            _countryAppService = countryAppService;
            _currencyAppService = currencyAppService;
            _billingTypeAppService = billingTypeAppService;
            _userRuleSetService = userRuleSetService;
        }

        public async Task<ActionResult> Index()
        {
            var roles = new List<ComboboxItemDto>();
            var permissions = _permissionAppService.GetAllPermissions()
                                                    .Items
                                                    .Select(p => new ComboboxItemDto(p.Name, new string('-', p.Level * 2) + " " + p.DisplayName))
                                                    .ToList();

            if (IsGranted(AppPermissions.Pages_Administration_Roles))
            {
                var getRolesOutput = await _roleAppService.GetRoles(new GetRolesInput());
                roles = getRolesOutput.Items.Select(r => new ComboboxItemDto(r.Id.ToString(), r.DisplayName)).ToList();
            }

            roles.Insert(0, new ComboboxItemDto("", ""));
            permissions.Insert(0, new ComboboxItemDto("", ""));

            var model = new UsersViewModel
            {
                FilterText = Request.Query["filterText"],
                Roles = roles,
                Permissions = permissions
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var output = await _userAppService.GetUserForEdit(new NullableIdDto<long> { Id = id });
            var viewModel = new CreateOrEditUserModalViewModel(output)
            {
                PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync()
            };
            ViewBag.Countries = new SelectList(_countryAppService.GetCountriesList().Countries, "Id", "Name");
            ViewBag.Currencies = new SelectList(_currencyAppService.GetCurrenciesList().Currencies, "Id", "Name");
            ViewBag.BillingTypes = new SelectList(_billingTypeAppService.GetBillingTypesList().BillingTypes, "Id", "Name");
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult UserRuleSets(long Id)
        {
            ViewBag.UserRuleSetId = Id;
            return PartialView("_UserRuleSets");
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task<PartialViewResult> PermissionsModal(long id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            var output = await _userAppService.GetUserPermissionsForEdit(new EntityDto<long>(id));
            var viewModel = new UserPermissionsEditViewModel(output, user);

            return PartialView("_PermissionsModal", viewModel);
        }

        public async Task<PartialViewResult> LoginAttemptsModal()
        {
            var output = await _userLoginAppService.GetRecentUserLoginAttempts();
            var model = new UserLoginAttemptModalViewModel
            {
                LoginAttempts = output.Items.ToList()
            };
            return PartialView("_LoginAttemptsModal", model);
        }
    }
}