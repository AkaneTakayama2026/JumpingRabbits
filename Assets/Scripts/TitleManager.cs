using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        ScoreManager.score = 0;

        SceneManager.LoadScene("1stStageScene");
    }
    public void OpenHowTo()
    {
        SceneManager.LoadScene("HowToScene");
    }
}
