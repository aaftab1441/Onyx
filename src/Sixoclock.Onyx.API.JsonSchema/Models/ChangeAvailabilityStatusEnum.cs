using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ChangeAvailabilityStatusEnum
    {

        [System.Runtime.Serialization.EnumMember(Value = "Accepted")]
        Accepted = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Rejected")]
        Rejected = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Scheduled")]
        Scheduled = 2,
    }
}
