namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum TransactionStatusEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Idle")]
        Idle,
        [System.Runtime.Serialization.EnumMember(Value = "Charging")]
        Charging,
        [System.Runtime.Serialization.EnumMember(Value = "Faulted")]
        Faulted,
        [System.Runtime.Serialization.EnumMember(Value = "Completed")]
        Completed
    }
}
