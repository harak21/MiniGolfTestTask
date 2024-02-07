using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.SaveLoad;
using MiniGolf.Utility;
using MiniGolf.Utility.Loading;

namespace MiniGolf.PlayerData
{
    [UsedImplicitly]
    public class PlayerDataContainer : ILoadUnit
    {
        private readonly ISaveLoadService _saveLoadService;
        
        public PlayerProgress PlayerProgress { get; private set; }
        public PlayerRuntimeData PlayerRuntimeData { get; private set; }

        public PlayerDataContainer(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public async Task Load()
        {
            PlayerProgress = 
                await _saveLoadService.LoadAsync<PlayerProgress>(RuntimeConstants.PlayerData.PlayerProgress) 
                ?? new PlayerProgress();
            PlayerRuntimeData = 
                await _saveLoadService.LoadAsync<PlayerRuntimeData>(RuntimeConstants.PlayerData.PlayerRuntimeData)
                ?? new PlayerRuntimeData();
        }
    }
}