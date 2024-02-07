using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGolf.UI.LevelHUD
{
    public class PauseView : MonoBehaviour
    {
        public event Action OnContinuePressed;
        public event Action OnRestartPressed;
        public event Action OnExitPressed;

        [SerializeField] private Button continueBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button exitBtn;

        private void Awake()
        {
            continueBtn.onClick.AddListener(Continue);
            restartBtn.onClick.AddListener(Restart);
            exitBtn.onClick.AddListener(Exit);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Continue()
        {
            OnContinuePressed?.Invoke();
        }

        private void Restart()
        {
            OnRestartPressed?.Invoke();
        }

        private void Exit()
        {
            OnExitPressed?.Invoke();
        }
    }
}