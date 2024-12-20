﻿using Abp.Dependency;

namespace Sixoclock.Onyx
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string TenantsFolder { get; set; }
    }
}