using System.Collections.Generic;
using Sixoclock.Onyx.Caching.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}