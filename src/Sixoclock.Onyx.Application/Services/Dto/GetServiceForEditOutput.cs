using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Services.Dto
{
    public class GetServiceForEditOutput
    {
        public ServiceDto Service { get; set; }

        public List<ComboboxItemDto> Features { get; set; }
    }
}
