using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int highScore;
    public Transform player;
    public TextMeshProUGUI scoreText;

    public static int score;

    private int bestHeight = 0;
    private static int bonusScore = 0;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
        bonusScore = 0;
    }

    void Update()
    {
        int currentHeight = Mathf.FloorToInt(player.position.y);

        if (currentHeight > bestHeight)
        {
            bestHeight = currentHeight;
        }

        scoreText.text = "Score : " + bestHeight;

        score = bestHeight + bonusScore;
        scoreText.text = "score : " + score;
    }
    public static void AddScore(int amount)
    {
        bonusScore += amount;
        Debug.Log("スコア加算: +" + amount + " / bonusScore =" + bonusScore);
    }

    public static void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

    }
}