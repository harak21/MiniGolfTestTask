using System.IO;
using UnityEngine;

namespace MiniGolf.Utility
{
    public static class RuntimeConstants
    {
        public static class Addressables
        {
            public const string LevelsConfigRepository = "LevelsConfigRepository";
            public const string MainMenu = "MainMenu";
            public const string GameHud = "GameHud";
            public const string PauseMenu = "PauseMenu";
            public const string LevelView = "LevelView";
            public const string Player = "Player";
        }
        
        public static class PlayerData
        {
            private static readonly string PlayerProgressPath;
            private static readonly string PlayerRuntimeDataPath;

            static PlayerData()
            {
                var persistentDataPath = Application.persistentDataPath;
                PlayerProgressPath = Path.Combine(persistentDataPath, "PlayerPregress");
                PlayerRuntimeDataPath = Path.Combine(persistentDataPath, "PlayerRuntimeDataPath");
            }

            public static readonly string PlayerProgress = PlayerProgressPath;
            public static readonly string PlayerRuntimeData = PlayerRuntimeDataPath;
        }
    }
}