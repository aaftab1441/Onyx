namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ChargingProfileKindEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Absolute")]
        Absolute = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Recurring")]
        Recurring = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Relative")]
        Relative = 2,
    }
}
