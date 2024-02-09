using JetBrains.Annotations;
using MiniGolf.Core.LevelObjects;
using MiniGolf.Core.Player;
using MiniGolf.Input;
using MiniGolf.PlayerData;
using MiniGolf.SceneManagement;
using MiniGolf.UI.LevelHUD;
using MiniGolf.Utility.Config;
using UnityEngine;

namespace MiniGolf.Core
{
    [UsedImplicitly]
    internal class LevelManager
    {
        private readonly LevelData _levelData;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly IInputSystem _inputSystem;
        private readonly LevelUiController _levelUiController;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerCharacterController _characterController;
        private readonly ConfigContainer _configContainer;

        private Vector2? _lastClickPosition;

        private bool _isPaused = true;
        private int _hitCount;

        public LevelManager(LevelData levelData, 
            PlayerDataContainer playerDataContainer, 
            IInputSystem inputSystem,
            LevelUiController levelUiController,
            ISceneLoadService sceneLoadService,
            PlayerCharacterController characterController,
            ConfigContainer configContainer)
        {
            _levelData = levelData;
            _playerDataContainer = playerDataContainer;
            _inputSystem = inputSystem;
            _levelUiController = levelUiController;
            _sceneLoadService = sceneLoadService;
            _characterController = characterController;
            _configContainer = configContainer;
        }

        public void Start()
        {
            _inputSystem.OnClickStarted += PlayerClickStarted;
            _inputSystem.OnClickPerformed += PlayerClickPerformed;
            _inputSystem.OnPausePressed += InputPausePressed;
            
            _levelUiController.Construct();
            _levelUiController.OnPausePressed += UiPausePressed;
            _levelUiController.OnContinuePressed += UiContinuePressed;
            _levelUiController.OnExitPressed += Exit;
            _levelUiController.OnRestartPressed += Restart;
            _levelUiController.OnNextPressed += NextLevel;

            _levelData.Hole.OnLevelFinish += FinishLevel;

            foreach (var booster in _levelData.Boosters)
            {
                booster.OnBallBust += BoostPlayer;
            }

            _isPaused = false;
            
            Application.quitting += ApplicationOnQuitting;
        }

        private void ApplicationOnQuitting()
        {
            _playerDataContainer.Save();
        }

        public void Dispose()
        {
            _inputSystem.OnClickStarted -= PlayerClickStarted;
            _inputSystem.OnClickPerformed -= PlayerClickPerformed;
            _inputSystem.OnPausePressed -= InputPausePressed;
            _levelUiController.OnPausePressed -= UiPausePressed;
            _levelUiController.OnContinuePressed -= UiContinuePressed;
            _levelUiController.OnExitPressed -= Exit;
            _levelUiController.OnRestartPressed -= Restart;
            _levelUiController.OnNextPressed -= NextLevel;

            _levelData.Hole.OnLevelFinish -= FinishLevel;
            
            foreach (var booster in _levelData.Boosters)
            {
                booster.OnBallBust -= BoostPlayer;
            }
        }
        
        public void Tick()
        {
            if (_isPaused)
                return;

            if (_lastClickPosition.HasValue)
            {
                var deltaVector = _lastClickPosition.Value - _inputSystem.PointerPosition;
                var rawForceDirection = new Vector3(deltaVector.x, 0, deltaVector.y);
                _characterController.SetForcePosition(rawForceDirection);
            }
        }

        private void FinishLevel()
        {
            _isPaused = true;
            var sceneConfig = _configContainer.LevelsConfigRepository[_playerDataContainer.PlayerRuntimeData.LastLevelID];
            var starCount = sceneConfig.Goals.threeStars >= _hitCount ? 3
                : sceneConfig.Goals.twoStars >= _hitCount ? 2
                : sceneConfig.Goals.oneStar >= _hitCount ? 1 : 0;
            if (starCount > 0)
            {
                if (_playerDataContainer.PlayerProgress.LevelsStars.TryGetValue(sceneConfig.ID, out var oldStart))
                {
                    _playerDataContainer.PlayerProgress.LevelsStars[sceneConfig.ID] = Mathf.Max(oldStart, starCount);
                }
                else
                {
                    _playerDataContainer.PlayerProgress.LevelsStars.Add(sceneConfig.ID, starCount);
                }
                _playerDataContainer.Save();
            }
            _levelUiController.ShowEndView(starCount);
        }

        private void NextLevel()
        {
            var levelsCount = _configContainer.LevelsConfigRepository.LevelsConfig.Count;
            var nextLevelIndex = _playerDataContainer.PlayerRuntimeData.LastLevelIndex + 1;
            var nextScene = _configContainer.LevelsConfigRepository.MainMenu;
            if (levelsCount > nextLevelIndex)
            {
                nextScene = _configContainer.LevelsConfigRepository.LevelsConfig[nextLevelIndex];
                _playerDataContainer.PlayerRuntimeData.LastLevelID = nextScene.ID;
                _playerDataContainer.PlayerRuntimeData.LastLevelIndex = nextLevelIndex;
            }
            _sceneLoadService.LoadScene(nextScene);
        }

        private void Restart()
        {
            _characterController.Restart();
            ChangeHitCount(0);
            ChangePauseState(false);
        }

        private void ChangeHitCount(int newHitCount)
        {
            _hitCount = newHitCount;
            _levelUiController.SetHitCount(_hitCount);
        }

        private void Exit()
        {
            //_playerDataContainer.PlayerRuntimeData.LastLevel = null;
            _playerDataContainer.Save();
            _sceneLoadService.LoadMainMenu();
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
                _levelUiController.Hide();
            }
        }

        private void PlayerClickStarted(Vector2 clickPosition)
        {
            if (_isPaused)
                return;
            _lastClickPosition = clickPosition;
        }

        private void PlayerClickPerformed()
        {
            if (!_lastClickPosition.HasValue)
                return;
            
            ChangeHitCount(_hitCount+1);
            
            var lastPointerPos = _inputSystem.PointerPosition;
            var deltaVector = _lastClickPosition.Value - lastPointerPos;
            var rawForceDirection = new Vector3(deltaVector.x, 0, deltaVector.y);
            _characterController.AddForce(rawForceDirection);
            _lastClickPosition = null;
            _characterController.ResetArrow();
        }
        
        private void BoostPlayer(Vector3 force)
        {
            _characterController.AddForce(force);
        }
    }
}