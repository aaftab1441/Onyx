using Abp.AutoMapper;

namespace Sixoclock.Onyx.Installs.Dto
{
    [AutoMapTo(typeof(Install))]
    public class CreateOrUpdateInstallInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int RegionId { get; set; }
        public string InstallName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
