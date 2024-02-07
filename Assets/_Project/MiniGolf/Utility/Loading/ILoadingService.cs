using System.Threading.Tasks;

namespace MiniGolf.Utility.Loading
{
    public interface ILoadingService
    {
        Task Load(ILoadUnit loadUnit);
    }
}