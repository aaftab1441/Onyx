using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sixoclock.Onyx.Grants
{
    public class RuleableAttribute:Attribute
    {
        public RuleableAttribute(Type type, [CallerMemberName]string name=null)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public Type Type { get; set; }

    }
}
