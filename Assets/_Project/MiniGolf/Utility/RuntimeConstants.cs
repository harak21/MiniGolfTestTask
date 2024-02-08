using System.IO;
using UnityEngine;

namespace MiniGolf.Utility
{
    public static class RuntimeConstants
    {
        public static class Addressables
        {
            public const string LevelsConfigRepository = "LevelsConfigRepository";
            public const string PlayerConfig = "PlayerConfig";
            public const string MainMenu = "MainMenu";
            public const string GameHud = "GameHud";
            public const string PauseMenu = "PauseMenu";
            public const string EndLevelView = "EndLevelView";
            public const string LevelView = "LevelView";
            public const string Player = "Player";
            public const string PlayerArrow = "PlayerArrow";
        }
        
        public static class PlayerData
        {
            static PlayerData()
            {
                var persistentDataPath = Application.persistentDataPath;
                PlayerProgress = Path.Combine(persistentDataPath, "PlayerPregress");
                PlayerRuntimeData = Path.Combine(persistentDataPath, "PlayerRuntimeData");
            }

            public static string PlayerProgress { get; }

            public static string PlayerRuntimeData { get; }
        }
    }
}