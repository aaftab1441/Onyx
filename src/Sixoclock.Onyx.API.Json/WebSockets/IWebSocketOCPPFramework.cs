using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.API.Json.WebSockets
{
    public interface IWebSocketOcppFramework
    {
        Task ProcessMessageAsync(string message, string connectionId);

        Task SendMessageAsync(string message, string connectionId,Dictionary<string,string> metadata);
        //Task SendMessageAsync(string message, string )
    }
}
