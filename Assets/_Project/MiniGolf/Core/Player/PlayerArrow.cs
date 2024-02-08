using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using UnityEngine;

namespace MiniGolf.Core.Player
{
    [UsedImplicitly]
    internal class PlayerArrow : ILoadUnit
    {
        private readonly ConfigContainer _configContainer;
        private LineRenderer _playerArrow;
        private static readonly int Color = Shader.PropertyToID("_Color");

        public PlayerArrow(ConfigContainer configContainer)
        {
            _configContainer = configContainer;
        }
        
        public async Task Load()
        {
            var arrowAsset = await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.PlayerArrow);
            _playerArrow = Object.Instantiate(arrowAsset).GetComponent<LineRenderer>();
        }

        public void SetLinePosition(Vector3 playerPos, Vector3 linePos)
        {
            _playerArrow.SetPosition(0, playerPos);
            _playerArrow.SetPosition(1, linePos);
            var value = Mathf.InverseLerp(0, 4f, (playerPos - linePos).magnitude);
            _playerArrow.material.SetColor(Color,_configContainer.PlayerConfig.ArrowGradient.Evaluate(value));
        }
    }
}