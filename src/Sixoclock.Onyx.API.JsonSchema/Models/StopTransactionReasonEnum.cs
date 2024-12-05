namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum StopTransactionReasonEnum
    {

            [System.Runtime.Serialization.EnumMember(Value = "EmergencyStop")]
            EmergencyStop = 0,

            [System.Runtime.Serialization.EnumMember(Value = "EVDisconnected")]
            EVDisconnected = 1,

            [System.Runtime.Serialization.EnumMember(Value = "HardReset")]
            HardReset = 2,

            [System.Runtime.Serialization.EnumMember(Value = "Local")]
            Local = 3,

            [System.Runtime.Serialization.EnumMember(Value = "Other")]
            Other = 4,

            [System.Runtime.Serialization.EnumMember(Value = "PowerLoss")]
            PowerLoss = 5,

            [System.Runtime.Serialization.EnumMember(Value = "Reboot")]
            Reboot = 6,

            [System.Runtime.Serialization.EnumMember(Value = "Remote")]
            Remote = 7,

            [System.Runtime.Serialization.EnumMember(Value = "SoftReset")]
            SoftReset = 8,

            [System.Runtime.Serialization.EnumMember(Value = "UnlockCommand")]
            UnlockCommand = 9,

            [System.Runtime.Serialization.EnumMember(Value = "DeAuthorized")]
            DeAuthorized = 10,

        
    }
}
