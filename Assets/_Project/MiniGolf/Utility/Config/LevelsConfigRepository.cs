using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MiniGolf.Utility.Config
{
    [CreateAssetMenu(fileName = "LevelsConfigRepository", menuName = "MiniGolf/LevelsConfigRepository", order = 0)]
    public class LevelsConfigRepository : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> levelsConfig = new();
        [SerializeField] private LevelConfig mainMenu;

        public ReadOnlyCollection<LevelConfig> LevelsConfig => levelsConfig.AsReadOnly();
        public LevelConfig MainMenu => mainMenu;

        public LevelConfig this[int id] => levelsConfig.Find(c => c.ID == id);
    }
}