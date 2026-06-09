using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int platformCount = 20;

    public float minX = -4f;
    public float maxX = 4f;

    public float startY = 2f;
    public float ySpacing = 2.5f;

    void Start()
    {
        for (int i = 0; i < platformCount; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = startY + i * ySpacing;

            Instantiate(
                platformPrefab,
                new Vector3(x, y, 0),
                Quaternion.identity
                );
        }

    }

}
