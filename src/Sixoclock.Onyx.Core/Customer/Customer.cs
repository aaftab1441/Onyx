using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;
using Sixoclock.Onyx.MultiTenancy;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Customer))]
    public class Customer : FullAuditedEntity, IMustHaveTenant
    {
        [Ruleable(typeof(string), "Name")]
        public string CustomerName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }
        [Ruleable(typeof(string))]
        public string ZipCode { get; set; }
        [Ruleable(typeof(string))]
        public string City { get; set; }
        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Comment { get; set; }

        public int SegmentId { get; set; }

        public int? CountryId { get; set; }

        public int TenantId { get; set; }

        //tenant which acts as customer
        public int CustomerTenantId { get; set; }

        public Tenant CustomerTenant { get; set; }

        public virtual ICollection<Market> Markets { get; set; }

        public virtual Segment Segment { get; set; }
        [Ruleable(typeof(Country))]
        public virtual Country Country { get; set; }
    }
}
