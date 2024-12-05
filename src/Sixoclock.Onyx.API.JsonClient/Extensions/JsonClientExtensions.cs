using Microsoft.Extensions.DependencyInjection;

namespace Sixoclock.Onyx.API.JsonClient.Extensions
{
    public static class JsonClientExtensions
    {
        public static void AddDeviceHub(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDeviceManager), typeof(DeviceManager));
        }
    }
}
