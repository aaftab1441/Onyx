using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sixoclock.Onyx.Grants.Domain
{
    public class RuleSetEntitiesProvider:OnyxDomainServiceBase, IRuleSetEntitiesProvider
    {
        public Dictionary<string, IEnumerable<Tuple<string, string>>> GetRuleSetEntities()
        {
            Dictionary<string,IEnumerable<Tuple<string, string>>> typesWithFields=new Dictionary<string, IEnumerable<Tuple<string, string>>>();
            var types = GetTypesWithRuleableAttribute(Assembly.GetAssembly(typeof(RuleSetEntitiesProvider)));
            foreach (var type in types)
            {
                List<Tuple<string,string>> props=new List<Tuple<string, string>>();
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    object[] attributes = prop.GetCustomAttributes(typeof(RuleableAttribute), true);
                    if (attributes.Length == 1)
                    {
                        props.Add(new Tuple<string,string>((attributes.First() as RuleableAttribute)?.Name, (attributes.First() as RuleableAttribute)?.Type.Name));
                    }
                }
                typesWithFields.Add(type.Name,props);
            }
            return typesWithFields;
        }
        private IEnumerable<Type> GetTypesWithRuleableAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(RuleableAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        public string GetRulablePropertyName(string entityName,string propertyAttribute)
        {
            var type = Assembly.GetAssembly(typeof(RuleSetEntitiesProvider)).GetTypes().FirstOrDefault(x=>x.Name==entityName);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                object[] attributes = prop.GetCustomAttributes(typeof(RuleableAttribute), true);
                if (attributes.Length == 1)
                {
                    if((attributes.First() as RuleableAttribute)?.Name==propertyAttribute)
                    return prop.Name;
                }
            }
            return string.Empty;
        }
    }
}
