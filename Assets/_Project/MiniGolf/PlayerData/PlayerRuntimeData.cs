using MiniGolf.SaveLoad;
using Newtonsoft.Json;

namespace MiniGolf.PlayerData
{
    public class PlayerRuntimeData : ISaveData
    {
        [JsonProperty] public int LastLevelIndex { get; set; }
        [JsonProperty] public int LastLevelID { get; set; } = -1;
    }
}