using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action = Unity.Plastic.Newtonsoft.Json.Serialization.Action;

namespace MiniGolf.UI.LevelHUD
{
    public class EndLevelView : MonoBehaviour
    {
        public event Action OnRestartPressed;
        public event Action OnExitPressed;
        public event Action OnNextPressed;
        
        [SerializeField] private List<GameObject> stars = new();
        [SerializeField] private Button exitBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button nextBtn;

        private void Awake()
        {
            exitBtn.onClick.AddListener(Exit);
            restartBtn.onClick.AddListener(Restart);
            nextBtn.onClick.AddListener(Next);
        }

        public void Show(int starCount)
        {
            for (int i = 0; i < starCount; i++)
            {
                stars[i].SetActive(true);
            }
            nextBtn.gameObject.SetActive(starCount >= 2);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Next()
        {
            OnNextPressed?.Invoke();
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