using System.Collections.Generic;
using Sixoclock.Onyx.Editions.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}