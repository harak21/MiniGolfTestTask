using System.Threading.Tasks;
using MiniGolf.Utility.Config;

namespace MiniGolf.SceneManagement
{
    public interface ISceneLoadService
    {
        Task LoadScene(LevelConfig levelConfig);
    }
}