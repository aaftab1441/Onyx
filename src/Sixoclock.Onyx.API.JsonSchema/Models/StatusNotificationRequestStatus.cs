namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum StatusNotificationRequestStatus
    {
        [System.Runtime.Serialization.EnumMember(Value = "Available")]
        Available = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Preparing")]
        Preparing = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Charging")]
        Charging = 2,

        [System.Runtime.Serialization.EnumMember(Value = "SuspendedEVSE")]
        SuspendedEVSE = 3,

        [System.Runtime.Serialization.EnumMember(Value = "SuspendedEV")]
        SuspendedEV = 4,

        [System.Runtime.Serialization.EnumMember(Value = "Finishing")]
        Finishing = 5,

        [System.Runtime.Serialization.EnumMember(Value = "Reserved")]
        Reserved = 6,

        [System.Runtime.Serialization.EnumMember(Value = "Unavailable")]
        Unavailable = 7,

        [System.Runtime.Serialization.EnumMember(Value = "Faulted")]
        Faulted = 8
    }
}
