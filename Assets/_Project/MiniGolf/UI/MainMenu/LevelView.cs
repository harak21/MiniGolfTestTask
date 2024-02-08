using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGolf.UI.MainMenu
{
    public class LevelView : MonoBehaviour
    {
        public event Action<int> OnLevelSelected;

        [SerializeField] private Button startBtn;
        [SerializeField] private TMP_Text label;
        [SerializeField] private Image background;
        [SerializeField] private List<Image> stars;
        
        [Space(5f)] [SerializeField] private Color availableColor;
        [SerializeField] private Color unavailableColor;
        
        [Space(5f)] [SerializeField] private Color starActiveColor;
        [SerializeField] private Color starInactiveColor;
        
        private int _levelIndex;

        public void SetLevel(string levelName, int levelIndex, bool isLevelAvailable, int starCount)
        {
            _levelIndex = levelIndex;
            label.text = levelName;

            if (!isLevelAvailable)
                return;

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].enabled = true;
                stars[i].color = i < starCount ? starActiveColor : starInactiveColor;
            }

            SetAvailable();
        }

        private void SetAvailable()
        {
            background.color = availableColor;
            startBtn.interactable = true;
            startBtn.onClick.AddListener(StartLevel);
        }

        private void StartLevel()
        {
            OnLevelSelected?.Invoke(_levelIndex);
        }
    }
}