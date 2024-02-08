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

        public MainMenuHandler(MainMenuUiController mainMenuUiController, 
            ISceneLoadService sceneLoadService,
            PlayerDataContainer playerDataContainer)
        {
            _mainMenuUiController = mainMenuUiController;
            _sceneLoadService = sceneLoadService;
            _playerDataContainer = playerDataContainer;
        }

        public void Start()
        {
            _mainMenuUiController.Construct();
            _mainMenuUiController.OnLevelSelected += StartSelectedLevel;
        }

        private async void StartSelectedLevel(LevelConfig levelConfig)
        {
            _playerDataContainer.PlayerRuntimeData.LastLevel = levelConfig;
            _mainMenuUiController.ShowLoadingCurtain();
            await _sceneLoadService.LoadScene(levelConfig);
            _mainMenuUiController.Hide();
        }
    }
}