using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject settingsUI;

        private const string MasterVolume = "MasterVolume";
        private const float Multiplier = 20f;

        private void Awake()
        {
            volumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        private void Start()
        {
            volumeSlider.value = PlayerPrefs.GetFloat(MasterVolume);
            SetMusicVolume(volumeSlider.value);
        }

        private void SetMusicVolume(float volume)
        {
            var logVolume = Mathf.Log10(volume) * Multiplier;
            masterMixer.SetFloat(MasterVolume, logVolume);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(MasterVolume, volumeSlider.value);
        }

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
            settingsUI.SetActive(false);
        }
    }
}
