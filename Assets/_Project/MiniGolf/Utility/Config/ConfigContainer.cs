using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility.Loading;

namespace MiniGolf.Utility.Config
{
    [UsedImplicitly]
    public class ConfigContainer : ILoadUnit
    {
        public LevelsConfigRepository LevelsConfigRepository { get; private set; }
        public PlayerConfig PlayerConfig { get; private set; }
        
        public async Task Load()
        {
            LevelsConfigRepository = 
                await AssetService.R.Load<LevelsConfigRepository>(RuntimeConstants.Addressables.LevelsConfigRepository);
            PlayerConfig = 
                await AssetService.R.Load<PlayerConfig>(RuntimeConstants.Addressables.PlayerConfig);
        }
    }
}