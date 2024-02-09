using System;
using System.Threading.Tasks;
using MiniGolf.Utility.Loading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MiniGolf.Utility.Config
{
    [Serializable]
    public class LevelConfig : ILoadUnit
    {
        [SerializeField] private int id;
        [SerializeField] private string levelName;
        [SerializeField] private string addressableName;
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private LevelGoals levelGoals;

        public Vector3 StartPoint => startPoint;
        public LevelGoals Goals => levelGoals;
        public string LevelName => levelName;

        public int ID => id;

        public async Task Load()
        {
            await Addressables.LoadSceneAsync(addressableName).Task;
        }
    }

    [Serializable]
    public struct LevelGoals
    {
        public int threeStars;
        public int twoStars;
        public int oneStar;
    }
}