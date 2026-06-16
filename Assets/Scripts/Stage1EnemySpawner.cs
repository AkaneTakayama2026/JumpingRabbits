using UnityEngine;

public class Stage1EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnInterval = 3f;
    public float spawnHeight = 8f;
    public float spawnXRange = 7f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-spawnXRange, spawnXRange);

        Vector3 spawnPosition = new Vector3(
            randomX,
            player.position.y + spawnHeight,
            0f
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
