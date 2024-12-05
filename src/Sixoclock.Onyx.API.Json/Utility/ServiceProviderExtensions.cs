using System;
using System.Linq;
using System.Reflection;

namespace Sixoclock.Onyx.API.Json.Utility
{
 
        public static class ServiceProviderExtensions
        {
            public static object CreateInstance(this IServiceProvider provider, TypeInfo t)
            {
            ConstructorInfo constructor = t.GetConstructors().FirstOrDefault();

                if (constructor != null)
                {
                    object[] args = constructor
                        .GetParameters()
                        .Select(o => o.ParameterType)
                        .Select(provider.GetService)
                        .ToArray();

                return Activator.CreateInstance(t.AsType(), args);
                }

                return null;
            }
        
    }
}
