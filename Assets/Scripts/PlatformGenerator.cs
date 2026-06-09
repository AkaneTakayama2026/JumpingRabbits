using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int platformCount = 20;

    public float startY = 2f;

    public float minYSpacing = 1.5f;
    public float maxYSpacing = 2f;

    public float minX = -4f;
    public float maxX = 4f;

    public float minXDistance = 3f;
    public float maxXDistance = 5f;

    private int nextDirection = 1;

    void Start()
    {
        Vector3 lastPosition = new Vector3(0f, startY, 0f);

        Instantiate(platformPrefab, lastPosition, Quaternion.identity);

        nextDirection = Random.value < 0.5f ? -1 : 1;

        for (int i = 1; i < platformCount; i++)
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

            Instantiate(platformPrefab, newPosition, Quaternion.identity);

            lastPosition = newPosition;

            nextDirection *= -1;
        }

    }

}
