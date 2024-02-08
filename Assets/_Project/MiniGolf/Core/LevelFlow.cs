using System;
using JetBrains.Annotations;
using MiniGolf.Core.Player;
using MiniGolf.UI.LevelHUD;
using MiniGolf.Utility.Loading;
using VContainer.Unity;

namespace MiniGolf.Core
{
    [UsedImplicitly]
    internal class LevelFlow : IStartable, ITickable, IDisposable 
    {
        private readonly ILoadingService _loadingService;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly LevelUiController _levelUiController;
        private readonly LevelManager _levelManager;
        private readonly PlayerCharacterController _characterController;
        private readonly PlayerArrow _playerArrow;

        public LevelFlow(ILoadingService loadingService,
            IPlayerSpawner playerSpawner,
            LevelUiController levelUiController,
            LevelManager levelManager, 
            PlayerCharacterController characterController,
            PlayerArrow playerArrow)
        {
            _loadingService = loadingService;
            _playerSpawner = playerSpawner;
            _levelUiController = levelUiController;
            _levelManager = levelManager;
            _characterController = characterController;
            _playerArrow = playerArrow;
        }
        
        public async void Start()
        {
            await _loadingService.Load(_playerSpawner);
            await _loadingService.Load(_playerArrow);
            await _loadingService.Load(_levelUiController);
            await _loadingService.Load(_characterController);
            _levelManager.Start();
        }

        public void Tick()
        {
            _levelManager.Tick();
        }

        public void Dispose()
        {
            _levelManager.Dispose();
        }
    }
}