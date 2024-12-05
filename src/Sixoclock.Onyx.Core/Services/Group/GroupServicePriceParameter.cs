using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class GroupServicePriceParameter:FullAuditedEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int GroupServiceId { get; set; }
        public GroupService GroupService { get; set; }
    }
}
