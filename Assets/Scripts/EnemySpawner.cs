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
        if (ScoreManager.score >= 200)
        {
            spawnInterval = 0.2f;
        }
        else if (ScoreManager.score >= 100)
        {
            spawnInterval = 0.3f;
        }
        else if (ScoreManager.score >= 60)
        {
            spawnInterval = 0.5f;
        }
        else
        {
            spawnInterval = 1.0f;
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
