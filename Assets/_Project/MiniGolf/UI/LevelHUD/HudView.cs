using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGolf.UI.LevelHUD
{
    public class HudView : MonoBehaviour
    {
        public event Action OnPausePressed;
        
        [SerializeField] private Button pauseBtn;
        [SerializeField] private TMP_Text hitsCountView;

        private void Awake()
        {
            pauseBtn.onClick.AddListener(Pause);
        }

        public void SetHitCount(int hitCount)
        {
            hitsCountView.text = hitCount.ToString();
        }

        private void Pause()
        {
            OnPausePressed?.Invoke();
        }
    }
}