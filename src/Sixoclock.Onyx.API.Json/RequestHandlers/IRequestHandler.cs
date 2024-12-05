using System.Threading.Tasks;
using Sixoclock.Onyx.API.JsonSchema.Models;

namespace Sixoclock.Onyx.API.Json.RequestHandlers
{
    public interface IRequestHandler
    {
        Task HandleRequestAsync(RPCMessage message);
    }
}
