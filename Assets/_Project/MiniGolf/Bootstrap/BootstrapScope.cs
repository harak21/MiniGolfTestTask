using MiniGolf.Core;
using MiniGolf.PlayerData;
using MiniGolf.Input;
using MiniGolf.SaveLoad;
using MiniGolf.SceneManagement;
using MiniGolf.UI.MainMenu;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using VContainer;
using VContainer.Unity;

namespace MiniGolf.Bootstrap
{
    internal class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            IsRoot = true;
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Scoped);
            builder.Register<IInputSystem, InputSystem>(Lifetime.Singleton);
            builder.Register<ILoadingService, LoadingService>(Lifetime.Singleton);
            builder.Register<ConfigContainer>(Lifetime.Singleton);
            builder.Register<ISceneLoadService, SceneLoadService>(Lifetime.Singleton);
            builder.Register<MainMenuUiController>(Lifetime.Scoped);
            builder.Register<PlayerDataContainer>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}