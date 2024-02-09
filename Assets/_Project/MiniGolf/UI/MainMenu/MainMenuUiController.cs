using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.PlayerData;
using MiniGolf.Utility;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MiniGolf.UI.MainMenu
{
    [UsedImplicitly]
    public class MainMenuUiController : ILoadUnit
    {
        public event Action<int> OnLevelSelected;
        
        private readonly ConfigContainer _configContainer;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly List<LevelView> _views = new();

        private MainMenuView _mainMenuView;
        private LevelView _levelViewPrefab;


        public MainMenuUiController(ConfigContainer configContainer, 
            PlayerDataContainer playerDataContainerContainer)
        {
            _configContainer = configContainer;
            _playerDataContainer = playerDataContainerContainer;
        }
        
        public async Task Load()
        {
            var menuAsset = await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.MainMenu);
            _mainMenuView = Object.Instantiate(menuAsset).GetComponent<MainMenuView>();
            _levelViewPrefab = (await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.LevelView)).GetComponent<LevelView>();
        }

        public void Construct()
        {
            _mainMenuView.OnContinueButtonClicked += StartLastAvailableLevel;
            
            var levelsConfig = _configContainer.LevelsConfigRepository.LevelsConfig;
            var levelsStars = _playerDataContainer.PlayerProgress.LevelsStars;

            
            for (var i = 0; i < levelsConfig.Count; i++)
            {
                bool isAvailable = true;
                int starCount = 0;
                
                if (i != 0)
                {
                    isAvailable = levelsStars.Count >= i && levelsStars[i - 1] >= 2;
                }
                starCount = levelsStars.Count > i ? levelsStars[i] : 0;
                
                ConstructLevelView(levelsConfig[i].LevelName, i, isAvailable, starCount);
            }
        }

        public void Show()
        {
            _mainMenuView.Show();
        }
        
        public void ShowLoadingCurtain()
        {
            _mainMenuView.ShowLoadingCurtain();
        }

        public void Hide()
        {
            _mainMenuView.Hide();
            _mainMenuView.HideLoadingCurtain();
        }

        private void ConstructLevelView(string levelName, int levelIndex, bool isAvailable, int starCount)
        {
            var levelView = Object.Instantiate(_levelViewPrefab, _mainMenuView.LevelsViewHandler);
            levelView.SetLevel(levelName, levelIndex, isAvailable, starCount);
            levelView.OnLevelSelected += StartSelectedLevel;
            _views.Add(levelView);
        }

        private void StartSelectedLevel(int levelIndex)
        {
            var levelConfig = _configContainer.LevelsConfigRepository.LevelsConfig[levelIndex];
            OnLevelSelected?.Invoke(levelConfig.ID);
        }

        private void StartLastAvailableLevel()
        {
            var lastLevelID = _playerDataContainer.PlayerRuntimeData.LastLevelID;
            LevelConfig lastLevel;
            if (lastLevelID == -1)
            {
                var lastOpenedLevel = _playerDataContainer.PlayerProgress.LevelsStars.Count;
                if (lastOpenedLevel >= _configContainer.LevelsConfigRepository.LevelsConfig.Count)
                {
                    lastOpenedLevel = _configContainer.LevelsConfigRepository.LevelsConfig.Count - 1;
                }

                lastLevel = _configContainer.LevelsConfigRepository.LevelsConfig[lastOpenedLevel];
            }
            else
            {
                lastLevel = _configContainer.LevelsConfigRepository[lastLevelID];
            }

            OnLevelSelected?.Invoke(lastLevel.ID);
        }
    }
}