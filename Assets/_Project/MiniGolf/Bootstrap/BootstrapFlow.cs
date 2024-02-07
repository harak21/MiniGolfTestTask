using MiniGolf.Core;
using MiniGolf.PlayerData;
using JetBrains.Annotations;
using MiniGolf.Input;
using MiniGolf.UI;
using MiniGolf.UI.MainMenu;
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
        private readonly MainMenuController _mainMenuController;
        private readonly GameManager _gameManager;
        private readonly IInputSystem _inputSystem;

        public BootstrapFlow(ILoadingService loadingService, 
            ConfigContainer configContainer, 
            PlayerDataContainer playerDataContainer,
            MainMenuController mainMenuController,
            GameManager gameManager)
        {
            _loadingService = loadingService;
            _configContainer = configContainer;
            _playerDataContainer = playerDataContainer;
            _mainMenuController = mainMenuController;
            _gameManager = gameManager;
        }
        
        public async void Start()
        {
            await _loadingService.Load(_configContainer);
            await _loadingService.Load(_playerDataContainer);
            await _loadingService.Load(_mainMenuController);
            _gameManager.Start();
        }
    }
}