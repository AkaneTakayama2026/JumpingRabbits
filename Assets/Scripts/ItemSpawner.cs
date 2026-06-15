using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject recoveryItemPrefab;

    public Transform player;

    public float spawnInterval = 8f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            SpawnItem();

            timer = 0f;
        }
    }

    void SpawnItem()
    {
        float randomX = Random.Range(-7f, 7f);

        Vector3 spawnPosition =
            new Vector3
            (randomX,
            player.position.y + 10f,
            0f);

        Instantiate(
            recoveryItemPrefab,
            spawnPosition,
            Quaternion.identity);
    }


}
