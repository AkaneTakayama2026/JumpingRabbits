using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Score : " + ScoreManager.score;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (ScoreManager.score >= highScore)
        {
            highScoreText.text = "NEW RECORD!\nHigh Score :" + highScore;
        }
        else
        {
            highScoreText.text = "High Score : " + highScore;
        }

    }

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
