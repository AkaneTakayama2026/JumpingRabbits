using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //保存されている最高スコア
    public static int highScore;
    //プレイヤーの位置を取得するためのTransform
    public Transform player;
    //スコア表示用UI
    public TextMeshProUGUI scoreText;
    //現在の総合スコア
    public static int score;
    //プレイヤーが到達した最高高度
    private int bestHeight = 0;
    //敵撃破などによるボーナススコア
    private static int bonusScore = 0;

    private void Start()
    {
        //保存済みのハイスコアを読み込む
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //ゲーム開始時にスコアを初期化
        score = 0;
        bonusScore = 0;
    }

    void Update()
    {
        //現在のプレイヤー高度を取得
        int currentHeight = Mathf.FloorToInt(player.position.y);

        //これまでの最高到達高度を更新
        if (currentHeight > bestHeight)
        {
            bestHeight = currentHeight;
        }



        //到達高度とボーナススコアを合算
        score = bestHeight + bonusScore;
        //UIへ現在のスコアを表示
        scoreText.text = "score : " + score;
    }
    public static void AddScore(int amount)
    {
        //敵撃破時などにボーナススコアを加算
        bonusScore += amount;
        Debug.Log("スコア加算: +" + amount + " / bonusScore =" + bonusScore);
    }

    public static void SaveHighScore()
    {
        //現在のスコアがハイスコアを超えている場合のみ更新
        if (score > highScore)
        {
            highScore = score;

            //ハイスコアを保存
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

    }
}