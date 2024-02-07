using MiniGolf.SaveLoad;
using MiniGolf.Utility.Config;
using Newtonsoft.Json;

namespace MiniGolf.PlayerData
{
    public class PlayerRuntimeData : ISaveData
    {
        [JsonProperty] public int LastLevelIndex { get; }
        [JsonProperty] public LevelConfig LastLevel { get; set; }
    }
}