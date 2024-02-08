using JetBrains.Annotations;
using MiniGolf.UI.MainMenu;
using MiniGolf.Utility.Loading;
using VContainer.Unity;

namespace MiniGolf.MainMenu
{
    [UsedImplicitly]
    public class MainMenuFlow : IStartable
    {
        private readonly MainMenuHandler _mainMenuHandler;
        private readonly ILoadingService _loadingService;
        private readonly MainMenuUiController _mainMenuUiController;

        public MainMenuFlow(MainMenuHandler mainMenuHandler, 
            ILoadingService loadingService,
            MainMenuUiController mainMenuUiController)
        {
            _mainMenuHandler = mainMenuHandler;
            _loadingService = loadingService;
            _mainMenuUiController = mainMenuUiController;
        }
        
        public async void Start()
        {
            await _loadingService.Load(_mainMenuUiController);
            _mainMenuHandler.Start();
        }
    }
}