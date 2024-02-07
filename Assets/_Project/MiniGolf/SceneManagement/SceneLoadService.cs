using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;

namespace MiniGolf.SceneManagement
{
    [UsedImplicitly]
    public class SceneLoadService : ISceneLoadService
    {
        private readonly ILoadingService _loadingService;

        public SceneLoadService(ILoadingService loadingService)
        {
            _loadingService = loadingService;
        }
        
        public Task LoadScene(LevelConfig levelConfig)
        {
            return _loadingService.Load(levelConfig);
        }
    }
}