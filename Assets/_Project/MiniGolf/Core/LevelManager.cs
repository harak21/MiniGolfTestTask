using JetBrains.Annotations;
using MiniGolf.Core.LevelObjects;
using MiniGolf.Core.Player;
using MiniGolf.Input;
using MiniGolf.PlayerData;
using MiniGolf.SceneManagement;
using MiniGolf.UI.LevelHUD;
using UnityEngine;

namespace MiniGolf.Core
{
    [UsedImplicitly]
    internal class LevelManager
    {
        private readonly LevelData _levelData;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly IInputSystem _inputSystem;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly LevelUiController _levelUiController;
        private readonly ISceneLoadService _sceneLoadService;

        private GameObject _player;
        private Vector2 _lastClickPosition;

        private bool _isPaused;

        public LevelManager(LevelData levelData, 
            PlayerDataContainer playerDataContainer, 
            IInputSystem inputSystem,
            IPlayerSpawner playerSpawner,
            LevelUiController levelUiController,
            ISceneLoadService sceneLoadService)
        {
            _levelData = levelData;
            _playerDataContainer = playerDataContainer;
            _inputSystem = inputSystem;
            _playerSpawner = playerSpawner;
            _levelUiController = levelUiController;
            _sceneLoadService = sceneLoadService;
        }

        public void Start()
        {
            _player = _playerSpawner.Spawn();
            _inputSystem.OnClickStarted += PlayerClickStarted;
            _inputSystem.OnClickPerformed += PlayerClickPerformed;
            _inputSystem.OnPausePressed += InputPausePressed;
            
            _levelUiController.Construct();
            _levelUiController.OnPausePressed += UiPausePressed;
            _levelUiController.OnContinuePressed += UiContinuePressed;
            _levelUiController.OnExitPressed += Exit;
            _levelUiController.OnRestartPressed += Restart;
        }

        private void Restart()
        {
            
        }

        private void Exit()
        {
            _sceneLoadService.LoadScene()
        }

        private void UiContinuePressed()
        {
            ChangePauseState(false);
        }

        private void UiPausePressed()
        {
            ChangePauseState(true);
        }

        private void InputPausePressed()
        {
            ChangePauseState(!_isPaused);
        }

        private void ChangePauseState(bool isPause)
        {
            _isPaused = isPause;
            if (_isPaused)
            {
                _levelUiController.ShowPauseView();
            }
            else
            {
                _levelUiController.HidePauseView();
            }
        }

        private void PlayerClickStarted(Vector2 clickPosition)
        {
            _lastClickPosition = clickPosition;
        }

        private void PlayerClickPerformed()
        {
            var forceDir = _inputSystem.PointerPosition;
            Debug.Log(forceDir - _lastClickPosition);
        }
    }
}