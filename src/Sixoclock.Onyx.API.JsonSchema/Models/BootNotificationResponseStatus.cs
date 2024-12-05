namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum BootNotificationResponseStatus
    {
        [System.Runtime.Serialization.EnumMember(Value = "Accepted")]
        Accepted = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Pending")]
        Pending = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Rejected")]
        Rejected = 2,

    }
}
