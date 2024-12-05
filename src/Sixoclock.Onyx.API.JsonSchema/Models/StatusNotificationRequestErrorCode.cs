namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum StatusNotificationRequestErrorCode
    {
        [System.Runtime.Serialization.EnumMember(Value = "ConnectorLockFailure")]
        ConnectorLockFailure = 0,

        [System.Runtime.Serialization.EnumMember(Value = "EVCommunicationError")]
        EVCommunicationError = 1,

        [System.Runtime.Serialization.EnumMember(Value = "GroundFailure")]
        GroundFailure = 2,

        [System.Runtime.Serialization.EnumMember(Value = "HighTemperature")]
        HighTemperature = 3,

        [System.Runtime.Serialization.EnumMember(Value = "InternalError")]
        InternalError = 4,

        [System.Runtime.Serialization.EnumMember(Value = "LocalListConflict")]
        LocalListConflict = 5,

        [System.Runtime.Serialization.EnumMember(Value = "NoError")]
        NoError = 6,

        [System.Runtime.Serialization.EnumMember(Value = "OtherError")]
        OtherError = 7,

        [System.Runtime.Serialization.EnumMember(Value = "OverCurrentFailure")]
        OverCurrentFailure = 8,

        [System.Runtime.Serialization.EnumMember(Value = "PowerMeterFailure")]
        PowerMeterFailure = 9,

        [System.Runtime.Serialization.EnumMember(Value = "PowerSwitchFailure")]
        PowerSwitchFailure = 10,

        [System.Runtime.Serialization.EnumMember(Value = "ReaderFailure")]
        ReaderFailure = 11,

        [System.Runtime.Serialization.EnumMember(Value = "ResetFailure")]
        ResetFailure = 12,

        [System.Runtime.Serialization.EnumMember(Value = "UnderVoltage")]
        UnderVoltage = 13,

        [System.Runtime.Serialization.EnumMember(Value = "OverVoltage")]
        OverVoltage = 14,

        [System.Runtime.Serialization.EnumMember(Value = "WeakSignal")]
        WeakSignal = 15
    }
}
