using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx;

namespace Sixoclock.Onyx
{

    public class CustomerService:FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<CustomerServicePriceParameter> CustomerServicePriceParameters { get; set; }
    }

}
