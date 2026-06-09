using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private int bestHeight = 0;

    void Update()
    {
        int currentHeight = Mathf.FloorToInt(player.position.y);

        if (currentHeight > bestHeight)
        {
            bestHeight = currentHeight;
        }

        scoreText.text = "Score : " + bestHeight;
    }
}