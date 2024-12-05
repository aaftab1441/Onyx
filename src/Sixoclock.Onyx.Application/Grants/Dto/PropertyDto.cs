using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sixoclock.Onyx.Grants.Dto
{
    public class PropertyDto:ComboboxItemDto
    {
        public string Type { get; set; }
    }
}
