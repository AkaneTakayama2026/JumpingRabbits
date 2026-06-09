
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;

    public int startPlatformCount = 20;

    public float startY = 2f;

    public float minYSpacing = 1.3f;
    public float maxYSpacing = 1.8f;

    public float minX = -7f;
    public float maxX = 7f;

    public float minXDistance = 4f;
    public float maxXDistance = 6f;

    public float generateAheadDistance = 20f;
    public float deleteBelowDistance = 12f;

    private List<GameObject> platforms = new List<GameObject>();

    private Vector3 lastPosition;
    private int nextDirection;

    void Start()
    {
        lastPosition = new Vector3(0f, startY, 0f);
        CreatePlatform(lastPosition);

        nextDirection = Random.value < 0.5f ? -1 : 1;

        for (int i = 1; i < startPlatformCount; i++)
        {
            GenerateNextPlatform();
        }
    }

    void Update()
    {
        if (player.position.y + generateAheadDistance > lastPosition.y)
        {
            GenerateNextPlatform();
        }

        DeleteOldPlatforms();
    }

    void GenerateNextPlatform()
    {
        if (lastPosition.x > maxX - minXDistance)
        {
            nextDirection = -1;
        }
        else if (lastPosition.x < minX + minXDistance)
        {
            nextDirection = 1;
        }

        float xDistance = Random.Range(minXDistance, maxXDistance) * nextDirection;

        float x = lastPosition.x + xDistance;
        x = Mathf.Clamp(x, minX, maxX);

        float y = lastPosition.y + Random.Range(minYSpacing, maxYSpacing);

        Vector3 newPosition = new Vector3(x, y, 0f);

        CreatePlatform(newPosition);

        lastPosition = newPosition;

        nextDirection *= -1;
    }

    void CreatePlatform(Vector3 position)
    {
        GameObject newPlatform = Instantiate(platformPrefab, position, Quaternion.identity);
        platforms.Add(newPlatform);
    }

    void DeleteOldPlatforms()
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i] == null) continue;

            if (platforms[i].transform.position.y < player.position.y - deleteBelowDistance)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}