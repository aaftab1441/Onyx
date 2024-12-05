﻿using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Sixoclock.Onyx.Configuration;

namespace Sixoclock.Onyx.Tests.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(OnyxTestModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
