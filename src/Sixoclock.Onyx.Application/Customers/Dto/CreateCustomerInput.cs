using Abp.AutoMapper;

namespace Sixoclock.Onyx
{
    [AutoMapTo(typeof(Customer))]
    public class CreateCustomerInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int? CountryId { get; set; }
        public int SegmentId { get; set; }

        public virtual string CustomerName { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string City { get; set; }

        public virtual string Phone1 { get; set; }

        public virtual string Phone2 { get; set; }
        public virtual string TenancyName { get; set; }
        public string AdminEmailAddress { get; set; }
        public bool SetRandomPassword { get; set; }
        public string AdminPassword { get; set; }
        public string AdminPasswordRepeat { get; set; }
    }
}