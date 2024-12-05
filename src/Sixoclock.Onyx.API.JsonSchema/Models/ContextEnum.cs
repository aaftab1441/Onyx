namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum ContextEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Interruption.Begin")]
        Interruption_Begin = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Interruption.End")]
        Interruption_End = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Sample.Clock")]
        Sample_Clock = 2,

        [System.Runtime.Serialization.EnumMember(Value = "Sample.Periodic")]
        Sample_Periodic = 3,

        [System.Runtime.Serialization.EnumMember(Value = "Transaction.Begin")]
        Transaction_Begin = 4,

        [System.Runtime.Serialization.EnumMember(Value = "Transaction.End")]
        Transaction_End = 5,

        [System.Runtime.Serialization.EnumMember(Value = "Trigger")]
        Trigger = 6,

        [System.Runtime.Serialization.EnumMember(Value = "Other")]
        Other = 7,
    }
}
