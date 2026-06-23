using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    //生成する背景画像のプレハブ
    public GameObject backgroundPrefab;
    //プレイヤーの位置を取得するためのTransform
    public Transform player;

    //背景１枚分の高さ
    public float backgroundHeight = 10f;
    //ゲーム開始時に生成する背景枚数
    public int startCount = 5;

    //プレイヤのどれくらい先まで背景を生成するか
    public float generateAheadDistance = 30f;

    //プレイヤから一定距離下に離れた背景画像を削除する距離
    public float deleteBelowDistance = 20f;

    //生成済背景を管理するリスト
    private List<GameObject> backgrounds = new List<GameObject>();

    //最後に生成した背景のY座標
    private float lastY = 0f;
    //生成した背景枚数を管理
    private int createdBackgroundCount = 0;
    //元のスケールを保存
    private Vector3 baseScale;

    void Start()
    {
        //プレハブの初期スケールを保存
        baseScale = backgroundPrefab.transform.localScale;

        for (int i = 0; i < startCount; i++)
        {
            CreateBackground(i * backgroundHeight);
        }
    }

    void Update()
    {
        if (player.position.y + generateAheadDistance > lastY)
        {
            CreateBackground(lastY + backgroundHeight);
        }

        DeleteOldBackgrounds();
    }

    void CreateBackground(float y)
    {
        Vector3 position = new Vector3(0f, y, 0f);

        GameObject bg = Instantiate(
            backgroundPrefab,
            position,
            Quaternion.identity
        );

        Vector3 scale = baseScale;

        // 左右はランダム
        if (Random.value < 0.5f)
        {
            scale.x *= -1f;
        }

        // 上下は交互
        if (createdBackgroundCount % 2 == 1)
        {
            scale.y *= -1f;
        }

        bg.transform.localScale = scale;

        backgrounds.Add(bg);

        createdBackgroundCount++;

        lastY = y;
    }

    void DeleteOldBackgrounds()
    {
        for (int i = backgrounds.Count - 1; i >= 0; i--)
        {
            if (backgrounds[i] == null) continue;

            if (backgrounds[i].transform.position.y < player.position.y - deleteBelowDistance)
            {
                Destroy(backgrounds[i]);
                backgrounds.RemoveAt(i);
            }
        }
    }
}