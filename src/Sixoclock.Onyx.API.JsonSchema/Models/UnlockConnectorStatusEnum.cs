using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum UnlockConnectorStatusEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Unlocked")]
        Unlocked = 0,

        [System.Runtime.Serialization.EnumMember(Value = "UnlockFailed")]
        UnlockFailed = 1,

        [System.Runtime.Serialization.EnumMember(Value = "NotSupported")]
        NotSupported = 2,
    }
}
