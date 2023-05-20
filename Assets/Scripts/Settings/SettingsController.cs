using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private GameObject mainMenuUI;

        public void BackToMainMenu()
        {
            mainMenuUI.SetActive(true);
            CloseSettings();
        }

        public void BackToGame()
        {
            CloseSettings();
        }
        
        private void CloseSettings()
        {
            gameObject.SetActive(false);
        }
    }
}
