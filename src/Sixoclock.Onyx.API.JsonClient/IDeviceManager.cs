using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.API.JsonClient
{
    public interface IDeviceManager
    {
        Task<string> SendMessageToDeviceAsync<T>(string identity, int tenantId, T payload, Dictionary<string,string> metadata=null);
       
    }
}
