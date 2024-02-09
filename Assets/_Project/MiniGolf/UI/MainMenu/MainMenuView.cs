using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGolf.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnContinueButtonClicked;
        
        [SerializeField] private Button continueBtn;
        [SerializeField] private ScrollRect levelsViewHandler;
        [SerializeField] private GameObject loadingCurtain; 

        public RectTransform LevelsViewHandler => levelsViewHandler.content;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            continueBtn.onClick.AddListener(OnContinueClicked);
        }

        private void OnContinueClicked()
        {
            OnContinueButtonClicked?.Invoke();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ShowLoadingCurtain()
        {
            loadingCurtain.SetActive(true);
        }
        
        public void HideLoadingCurtain()
        {
            loadingCurtain.SetActive(false);
        }
    }
}