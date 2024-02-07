using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility;
using MiniGolf.Utility.Config;
using MiniGolf.Utility.Loading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MiniGolf.UI.LevelHUD
{
    [UsedImplicitly]
    public class LevelUiController : ILoadUnit
    {
        public event Action OnPausePressed;
        public event Action OnContinuePressed;
        public event Action OnRestartPressed;
        public event Action OnExitPressed;
        
        private HudView _hudView;
        private PauseView _pauseView;
        
        public async Task Load()
        {
            var hudAsset = await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.GameHud);
            _hudView = Object.Instantiate(hudAsset).GetComponent<HudView>();
            var pauseAsset = await AssetService.R.Load<GameObject>(RuntimeConstants.Addressables.PauseMenu);
            _pauseView = Object.Instantiate(pauseAsset).GetComponent<PauseView>();
        }

        public void SetHitCount(int count)
        {
            _hudView.SetHitCount(count);
        }

        public void ShowPauseView()
        {
            _pauseView.Show();
        }

        public void HidePauseView()
        {
            _pauseView.Hide();
        }

        public void Construct()
        {
            _hudView.OnPausePressed += Pause;
            _pauseView.OnContinuePressed += Continue;
            _pauseView.OnExitPressed += Exit;
            _pauseView.OnRestartPressed += Restart;
        }

        private void Restart()
        {
            OnRestartPressed?.Invoke();
        }

        private void Exit()
        {
            OnExitPressed?.Invoke();
        }

        private void Continue()
        {
            OnContinuePressed?.Invoke();
        }

        private void Pause()
        {
            OnPausePressed?.Invoke();
        }
    }
}