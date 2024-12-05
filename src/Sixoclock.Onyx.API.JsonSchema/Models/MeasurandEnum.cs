namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    public enum MeasurandEnum
    {
        [System.Runtime.Serialization.EnumMember(Value = "Energy.Active.Export.Register")]
        Energy_Active_Export_Register = 0,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Active.Import.Register")]
        Energy_Active_Import_Register = 1,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Reactive.Export.Register")]
        Energy_Reactive_Export_Register = 2,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Reactive.Import.Register")]
        Energy_Reactive_Import_Register = 3,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Active.Export.Interval")]
        Energy_Active_Export_Interval = 4,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Active.Import.Interval")]
        Energy_Active_Import_Interval = 5,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Reactive.Export.Interval")]
        Energy_Reactive_Export_Interval = 6,

        [System.Runtime.Serialization.EnumMember(Value = "Energy.Reactive.Import.Interval")]
        Energy_Reactive_Import_Interval = 7,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Active.Export")]
        Power_Active_Export = 8,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Active.Import")]
        Power_Active_Import = 9,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Offered")]
        Power_Offered = 10,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Reactive.Export")]
        Power_Reactive_Export = 11,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Reactive.Import")]
        Power_Reactive_Import = 12,

        [System.Runtime.Serialization.EnumMember(Value = "Power.Factor")]
        Power_Factor = 13,

        [System.Runtime.Serialization.EnumMember(Value = "Current.Import")]
        Current_Import = 14,

        [System.Runtime.Serialization.EnumMember(Value = "Current.Export")]
        Current_Export = 15,

        [System.Runtime.Serialization.EnumMember(Value = "Current.Offered")]
        Current_Offered = 16,

        [System.Runtime.Serialization.EnumMember(Value = "Voltage")]
        Voltage = 17,

        [System.Runtime.Serialization.EnumMember(Value = "Frequency")]
        Frequency = 18,

        [System.Runtime.Serialization.EnumMember(Value = "Temperature")]
        Temperature = 19,

        [System.Runtime.Serialization.EnumMember(Value = "SoC")]
        SoC = 20,

        [System.Runtime.Serialization.EnumMember(Value = "RPM")]
        RPM = 21,
    }
}
