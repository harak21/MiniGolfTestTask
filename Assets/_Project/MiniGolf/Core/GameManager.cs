using JetBrains.Annotations;
using MiniGolf.PlayerData;
using MiniGolf.SceneManagement;
using MiniGolf.UI;
using MiniGolf.UI.MainMenu;
using MiniGolf.Utility.Config;

namespace MiniGolf.Core
{
    [UsedImplicitly]
    public class GameManager
    {
        private readonly MainMenuController _mainMenuController;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerDataContainer _playerDataContainer;

        public GameManager(MainMenuController mainMenuController, 
            ISceneLoadService sceneLoadService,
            PlayerDataContainer playerDataContainer)
        {
            _mainMenuController = mainMenuController;
            _sceneLoadService = sceneLoadService;
            _playerDataContainer = playerDataContainer;
        }

        public void Start()
        {
            _mainMenuController.Construct();
            _mainMenuController.OnLevelSelected += StartSelectedLevel;
        }

        private async void StartSelectedLevel(LevelConfig levelConfig)
        {
            _playerDataContainer.PlayerRuntimeData.LastLevel = levelConfig;
            _mainMenuController.ShowLoadingCurtain();
            await _sceneLoadService.LoadScene(levelConfig);
            _mainMenuController.Hide();
        }
    }
}