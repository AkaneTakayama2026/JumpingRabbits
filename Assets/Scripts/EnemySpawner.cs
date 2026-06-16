using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject floatEnemyprefab;
    public Transform player;

    public float normalspawnInterval = 2f;
    public float hardspawnInterval = 2f;

    public float spawnHeight = 8f;

    private float timer;
    private float spawnInterval;


    void Update()
    {
        float height = player.transform.position.y;
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
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    void SpawnEnemy()
    {
        float randomX = Random.Range(-7f, 7f);

        Vector3 spawnPosition = new Vector3(
            randomX, player.position.y + spawnHeight, 0f);
        GameObject enemyToSpawn;

        if (Random.value < 0.5f)
        {
            enemyToSpawn = enemyPrefab;
        }
        else
        {
            enemyToSpawn = floatEnemyprefab;
        }
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}
