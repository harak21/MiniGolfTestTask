using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.PlayerData;
using MiniGolf.Utility;
using MiniGolf.Utility.Config;
using UnityEngine;

namespace MiniGolf.Core.Player
{
    [UsedImplicitly]
    internal class PlayerSpawner : IPlayerSpawner
    {
        private readonly LevelConfig _levelConfig;
        private GameObject _playerPrefab;

        public PlayerSpawner(PlayerDataContainer playerDataContainer)
        {
            _levelConfig = playerDataContainer.PlayerRuntimeData.LastLevel;
        }
        
        public async Task Load()
        {
            _playerPrefab = await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.Player);
            Debug.Log(_playerPrefab);
        }

        public GameObject Spawn()
        {
            return Object.Instantiate(_playerPrefab, _levelConfig.StartPoint, Quaternion.identity);
        }

        /// <summary>
        /// you can make a pool here if you need to.
        /// </summary>
        /// <param name="gameObject"></param>
        public void Release(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}