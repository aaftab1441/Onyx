using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ChangeAvailabilityTypeEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Inoperative")]
        Inoperative = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Operative")]
        Operative = 1,
    }
}
