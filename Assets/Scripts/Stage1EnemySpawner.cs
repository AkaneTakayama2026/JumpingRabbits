using UnityEngine;

public class Stage1EnemySpawner : MonoBehaviour
{
    //生成する敵のプレハブ
    public GameObject enemyPrefab;

    //プレイヤーの位置を取得するためのTransform
    public Transform player;

    //敵を生成する感覚
    public float spawnInterval = 3f;
    //プレイヤよりどれだけ上に敵を生成するか
    public float spawnHeight = 8f;
    //敵を生成するX座標の範囲
    public float spawnXRange = 7f;

    //適生成までの経過時間を管理するタイマー
    private float timer;

    void Update()
    {
        //経過時間を加算
        timer += Time.deltaTime;

        //一定時間経過したら敵を生成
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            //タイマーをリセット
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        //ランダムなX座標を決定
        float randomX = Random.Range(-spawnXRange, spawnXRange);
        //プレイヤーの少し上に敵を生成
        Vector3 spawnPosition = new Vector3(
            randomX,
            player.position.y + spawnHeight,
            0f
        );

        //敵を生成
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
