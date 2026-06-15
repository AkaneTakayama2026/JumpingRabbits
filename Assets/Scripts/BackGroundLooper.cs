using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public Transform player;

    public float backgroundHeight = 10f;
    public int startCount = 5;

    public float generateAheadDistance = 30f;
    public float deleteBelowDistance = 20f;

    private List<GameObject> backgrounds = new List<GameObject>();

    private float lastY = 0f;
    private int createdBackgroundCount = 0;
    private Vector3 baseScale;

    void Start()
    {
        baseScale = backgroundPrefab.transform.localScale;

        for (int i = 0; i < startCount; i++)
        {
            CreateBackground(i * backgroundHeight);
        }
    }

    void Update()
    {
        if (player.position.y + generateAheadDistance > lastY)
        {
            CreateBackground(lastY + backgroundHeight);
        }

        DeleteOldBackgrounds();
    }

    void CreateBackground(float y)
    {
        Vector3 position = new Vector3(0f, y, 0f);

        GameObject bg = Instantiate(
            backgroundPrefab,
            position,
            Quaternion.identity
        );

        Vector3 scale = baseScale;

        // 左右はランダム
        if (Random.value < 0.5f)
        {
            scale.x *= -1f;
        }

        // 上下は交互
        if (createdBackgroundCount % 2 == 1)
        {
            scale.y *= -1f;
        }

        bg.transform.localScale = scale;

        backgrounds.Add(bg);

        createdBackgroundCount++;

        lastY = y;
    }

    void DeleteOldBackgrounds()
    {
        for (int i = backgrounds.Count - 1; i >= 0; i--)
        {
            if (backgrounds[i] == null) continue;

            if (backgrounds[i].transform.position.y < player.position.y - deleteBelowDistance)
            {
                Destroy(backgrounds[i]);
                backgrounds.RemoveAt(i);
            }
        }
    }
}