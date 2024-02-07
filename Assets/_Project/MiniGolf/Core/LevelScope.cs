using MiniGolf.Core.LevelObjects;
using MiniGolf.Core.Player;
using MiniGolf.UI.LevelHUD;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MiniGolf.Core
{
    internal class LevelScope : LifetimeScope
    {
        [SerializeField] private LevelData levelData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(levelData);
            builder.Register<IPlayerSpawner, PlayerSpawner>(Lifetime.Scoped);
            builder.Register<LevelUiController>(Lifetime.Scoped);
            builder.Register<LevelManager>(Lifetime.Scoped);

            builder.RegisterEntryPoint<LevelFlow>();
        }
    }
}