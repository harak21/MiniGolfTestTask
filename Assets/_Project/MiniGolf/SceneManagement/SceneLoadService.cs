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
        private readonly ConfigContainer _configContainer;

        public SceneLoadService(ILoadingService loadingService, ConfigContainer configContainer)
        {
            _loadingService = loadingService;
            _configContainer = configContainer;
        }
        
        public Task LoadScene(LevelConfig levelConfig)
        {
            return _loadingService.Load(levelConfig);
        }

        public Task LoadMainMenu()
        {
            return _loadingService.Load(_configContainer.LevelsConfigRepository.MainMenu);
        }
    }
}