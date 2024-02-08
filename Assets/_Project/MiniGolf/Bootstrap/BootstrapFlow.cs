using MiniGolf.PlayerData;
using JetBrains.Annotations;
using MiniGolf.SceneManagement;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using VContainer.Unity;

namespace MiniGolf.Bootstrap
{
    [UsedImplicitly]
    internal class BootstrapFlow : IStartable
    {
        private readonly ILoadingService _loadingService;
        private readonly ConfigContainer _configContainer;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly ISceneLoadService _sceneLoadService;

        public BootstrapFlow(ILoadingService loadingService, 
            ConfigContainer configContainer, 
            PlayerDataContainer playerDataContainer,
            ISceneLoadService sceneLoadService)
        {
            _loadingService = loadingService;
            _configContainer = configContainer;
            _playerDataContainer = playerDataContainer;
            _sceneLoadService = sceneLoadService;
        }
        
        public async void Start()
        {
            await _loadingService.Load(_configContainer);
            await _loadingService.Load(_playerDataContainer);

            await _sceneLoadService.LoadMainMenu();
        }
    }
}