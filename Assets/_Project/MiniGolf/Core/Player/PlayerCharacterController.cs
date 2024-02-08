using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using UnityEngine;

namespace MiniGolf.Core.Player
{
    [UsedImplicitly]
    internal class PlayerCharacterController : ILoadUnit
    {
        private readonly IPlayerSpawner _playerSpawner;
        private readonly PlayerArrow _playerArrow;
        private readonly PlayerConfig _playerConfig;
        private Rigidbody _playerRb;
        private GameObject _player;

        public PlayerCharacterController(IPlayerSpawner playerSpawner, 
            ConfigContainer configContainer,
            PlayerArrow playerArrow)
        {
            _playerSpawner = playerSpawner;
            _playerArrow = playerArrow;
            _playerConfig = configContainer.PlayerConfig;
        }

        public Task Load()
        {
            _player = _playerSpawner.Spawn();
            _playerRb = _player.GetComponent<Rigidbody>();
            return Task.CompletedTask;
        }

        public void SetForcePosition(Vector3 forcePos)
        {
            var position = _player.transform.position;
            var force = 
                Vector3.ClampMagnitude(
                    forcePos * _playerConfig.InputForceMultiplier,
                    _playerConfig.MaxForce * 0.5f) / 200f;
            var linePos = position - force;
            _playerArrow.SetLinePosition(linePos, position);
        }

        public void AddForce(Vector3 rawForceDirection)
        {
            var force = 
                Vector3.ClampMagnitude(
                    rawForceDirection * _playerConfig.InputForceMultiplier,
                    _playerConfig.MaxForce);
            _playerRb.AddForce(force, ForceMode.Force);
        }

        public void Restart()
        {
            _playerSpawner.Release(_player);
            Load();
        }
    }
}