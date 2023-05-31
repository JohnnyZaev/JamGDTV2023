using UnityEngine;

public class CreditsEndGame : MonoBehaviour
{
    public void EndGame()
    {
        // TODO: delete after tests
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
