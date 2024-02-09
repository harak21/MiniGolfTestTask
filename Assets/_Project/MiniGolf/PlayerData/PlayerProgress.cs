using System.Collections.Generic;
using MiniGolf.SaveLoad;
using Newtonsoft.Json;

namespace MiniGolf.PlayerData
{
    public class PlayerProgress : ISaveData
    {
        [JsonProperty] public Dictionary<int, int> LevelsStars { get; set; } = new();
    }
}