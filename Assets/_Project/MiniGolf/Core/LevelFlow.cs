using JetBrains.Annotations;
using MiniGolf.Core.Player;
using MiniGolf.UI.LevelHUD;
using MiniGolf.Utility.Loading;
using VContainer.Unity;

namespace MiniGolf.Core
{
    [UsedImplicitly]
    internal class LevelFlow : IStartable
    {
        private readonly ILoadingService _loadingService;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly LevelUiController _levelUiController;
        private readonly LevelManager _levelManager;

        public LevelFlow(ILoadingService loadingService,
            IPlayerSpawner playerSpawner,
            LevelUiController levelUiController,
            LevelManager levelManager)
        {
            _loadingService = loadingService;
            _playerSpawner = playerSpawner;
            _levelUiController = levelUiController;
            _levelManager = levelManager;
        }
        
        public async void Start()
        {
            await _loadingService.Load(_playerSpawner);
            await _loadingService.Load(_levelUiController);
            _levelManager.Start();
        }
    }
}