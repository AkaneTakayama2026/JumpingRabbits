using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //通常の敵プレハブ
    public GameObject enemyPrefab;
    //浮遊verの敵プレハブ
    public GameObject floatEnemyprefab;
    //プレイヤの位置を取得するためのTransform
    public Transform player;

    //敵の出現間隔(調整用)
    public float normalspawnInterval = 2f;
    public float hardspawnInterval = 2f;

    //プレイヤよりどれだけ上に敵を生成するか
    public float spawnHeight = 8f;

    //出現タイマー
    private float timer;

    //現在適用されている出現間隔
    private float spawnInterval;


    void Update()
    {
        //プレイヤーの到達高度を取得
        float height = player.transform.position.y;

        //高度に応じて敵の出現間隔を変更し
        //上へ進むほど難易度も高くなるように設定
        if (height >= 200f)
        {
            spawnInterval = 0.7f;
        }
        else if (height >= 120f)
        {
            spawnInterval = 1.0f;
        }
        else if (height >= 60f)
        {
            spawnInterval = 1.5f;
        }
        else
        {
            spawnInterval = 2.0f;
        }

        //経過時間を計測
        timer += Time.deltaTime;

        //一定時間経過したら敵を生成
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    void SpawnEnemy()
    {
        //ランダムなX座標を決定
        float randomX = Random.Range(-7f, 7f);

        //プレイヤの少し上に敵を生成
        Vector3 spawnPosition = new Vector3(
            randomX, player.position.y + spawnHeight, 0f);

        GameObject enemyToSpawn;

        //2種類の敵からランダムで選択
        if (Random.value < 0.5f)
        {
            enemyToSpawn = enemyPrefab;
        }
        else
        {
            enemyToSpawn = floatEnemyprefab;
        }

        //敵を生成
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}
