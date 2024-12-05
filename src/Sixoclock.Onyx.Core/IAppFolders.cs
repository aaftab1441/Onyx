namespace Sixoclock.Onyx
{
    public interface IAppFolders
    {
        string TempFileDownloadFolder { get; }

        string SampleProfileImagesFolder { get; }

        string WebLogsFolder { get; set; }

        string TenantsFolder { get; set; }
    }
}