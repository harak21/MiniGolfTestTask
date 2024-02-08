using MiniGolf.UI.MainMenu;
using VContainer;
using VContainer.Unity;

namespace MiniGolf.MainMenu
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MainMenuUiController>(Lifetime.Scoped);
            builder.Register<MainMenuHandler>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<MainMenuFlow>(Lifetime.Scoped);
        }
    }
}