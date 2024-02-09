using System.Linq;
using JetBrains.Annotations;
using MiniGolf.PlayerData;
using MiniGolf.SceneManagement;
using MiniGolf.UI.MainMenu;
using MiniGolf.Utility.Config;

namespace MiniGolf.MainMenu
{
    [UsedImplicitly]
    public class MainMenuHandler
    {
        private readonly MainMenuUiController _mainMenuUiController;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerDataContainer _playerDataContainer;
        private readonly ConfigContainer _configContainer;

        public MainMenuHandler(MainMenuUiController mainMenuUiController, 
            ISceneLoadService sceneLoadService,
            PlayerDataContainer playerDataContainer,
            ConfigContainer configContainer)
        {
            _mainMenuUiController = mainMenuUiController;
            _sceneLoadService = sceneLoadService;
            _playerDataContainer = playerDataContainer;
            _configContainer = configContainer;
        }

        public void Start()
        {
            _mainMenuUiController.Construct();
            _mainMenuUiController.OnLevelSelected += StartSelectedLevel;
        }

        private async void StartSelectedLevel(int levelID)
        {
            var levelsConfig = _configContainer.LevelsConfigRepository.LevelsConfig;
            var level = levelsConfig.First(c => c.ID == levelID);
            _playerDataContainer.PlayerRuntimeData.LastLevelID = level.ID;
            _playerDataContainer.PlayerRuntimeData.LastLevelIndex = levelsConfig.IndexOf(level);
            _mainMenuUiController.ShowLoadingCurtain();
            await _sceneLoadService.LoadScene(level);
            _mainMenuUiController.Hide();
        }
    }
}