using System.Threading.Tasks;

namespace Sixoclock.Onyx.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}