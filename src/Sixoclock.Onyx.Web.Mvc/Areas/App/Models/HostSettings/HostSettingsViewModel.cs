using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Configuration.Host.Dto;
using Sixoclock.Onyx.Editions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}