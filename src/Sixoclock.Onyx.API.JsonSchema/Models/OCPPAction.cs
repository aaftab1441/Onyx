using System;
using System.ComponentModel;

namespace Sixoclock.Onyx.API.JsonSchema.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OCPPAction:Attribute
    {
        public string Name { get; set; }
        public RequestHandlerType Type { get; set; }
    }

    public enum RequestHandlerType
    {
        [Description("Send message from Central System to Chargepoint")]
        CSTOCP,
        [Description("Handle message from Chargepoint to Central System")]
        CPTOCS
    }
}
