namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ChargingProfilePurposeEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "ChargePointMaxProfile")]
        ChargePointMaxProfile = 0,

        [System.Runtime.Serialization.EnumMember(Value = "TxDefaultProfile")]
        TxDefaultProfile = 1,

        [System.Runtime.Serialization.EnumMember(Value = "TxProfile")]
        TxProfile = 2,
    }
}
