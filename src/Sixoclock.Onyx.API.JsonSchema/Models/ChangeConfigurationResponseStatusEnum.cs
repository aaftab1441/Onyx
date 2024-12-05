using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ChangeConfigurationResponseStatusEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Accepted")]
        Accepted = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Rejected")]
        Rejected = 1,

        [System.Runtime.Serialization.EnumMember(Value = "RebootRequired")]
        RebootRequired = 2,

        [System.Runtime.Serialization.EnumMember(Value = "NotSupported")]
        NotSupported = 3
    }
}
