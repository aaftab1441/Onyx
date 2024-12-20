﻿namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum PhaseEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "L1")]
        L1 = 0,

        [System.Runtime.Serialization.EnumMember(Value = "L2")]
        L2 = 1,

        [System.Runtime.Serialization.EnumMember(Value = "L3")]
        L3 = 2,

        [System.Runtime.Serialization.EnumMember(Value = "N")]
        N = 3,

        [System.Runtime.Serialization.EnumMember(Value = "L1-N")]
        L1N = 4,

        [System.Runtime.Serialization.EnumMember(Value = "L2-N")]
        L2N = 5,

        [System.Runtime.Serialization.EnumMember(Value = "L3-N")]
        L3N = 6,

        [System.Runtime.Serialization.EnumMember(Value = "L1-L2")]
        L1L2 = 7,

        [System.Runtime.Serialization.EnumMember(Value = "L2-L3")]
        L2L3 = 8,

        [System.Runtime.Serialization.EnumMember(Value = "L3-L1")]
        L3L1 = 9,
    }
}
