using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f;

        if (string.IsNullOrEmpty(SceneData.lastPlaySceneName))
        {
            SceneManager.LoadScene("2ndStageScene");
            return;
        }

        SceneManager.LoadScene(SceneData.lastPlaySceneName);
    }

    public void GoTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");

    }
}
