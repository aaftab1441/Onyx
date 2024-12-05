using System;

namespace Sixoclock.Onyx.Authorization.Users.Dto
{
    public class TagEditDto
    {
        public int TagId { get; set; }
        public int ParentTagId { get; set; }
        public string ParentTagValue { get; set; }
        public string IdToken { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
