using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject settingsUIObject;

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OpenSettings()
        {
            gameObject.SetActive(false);
            settingsUIObject.SetActive(true);
        }
    }
}
