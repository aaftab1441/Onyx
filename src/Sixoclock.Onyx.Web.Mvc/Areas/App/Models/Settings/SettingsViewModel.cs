using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Configuration.Tenants.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}