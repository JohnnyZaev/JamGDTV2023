using Input;
using UnityEngine;
using UnityEngine.Events;

namespace MainMenu
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject settingsMenuUI;
        [SerializeField] private UnityEvent onPaused;
        [SerializeField] private UnityEvent onUnpaused;

        private bool _isPaused;

        private void Awake()
        {
            onPaused.AddListener(Pause);
            onUnpaused.AddListener(Unpause);
        }

        private void Update()
        {
            if (!InputManager.Instance.MenuOpenCloseInput) return;
            if (!_isPaused)
            {
                onPaused.Invoke();
            }
            else
            {
                onUnpaused.Invoke();
            }
        }

        #region Pause/Unpause functions

        private void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0f;

            OpenMainMenu();
        }
        
        private void Unpause()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            
            CloseAllMenus();
        }

        public void BackToGame()
        {
            onUnpaused.Invoke();
        }

        #endregion

        #region CanvasActivations

        private void OpenMainMenu()
        {
            mainMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
        }

        private void CloseAllMenus()
        {
            mainMenuUI.SetActive(false);
            settingsMenuUI.SetActive(false);
        }

        #endregion
    }
}
