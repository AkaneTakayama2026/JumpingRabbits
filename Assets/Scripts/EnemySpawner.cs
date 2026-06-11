using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnInterval = 2f;
    public float spawnHeight = 8f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
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
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
