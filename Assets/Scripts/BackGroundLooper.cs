using System.Collections.Generic;
using UnityEngine;

public class BackGroundLooper : MonoBehaviour
{
    public GameObject backgroudPrefab;
    public Transform player;

    public float backgroundHeight = 10f;
    public int startCount = 5;

    public float generateAheadDistance = 30f;
    public float deleteBelowDistance = 20f;

    private List<GameObject> backgrounds = new List<GameObject>();
    private float lastY = 0f;
    void Start()
    {
        for (int i = 0; i < startCount; i++)
        {
            CreateBackGround(i * backgroundHeight);
        }
    }

    void Update()
    {
        if (player.position.y + generateAheadDistance > lastY)
        {
            CreateBackGround(lastY + backgroundHeight);
        }

        DeleteOldBackgrounds();
    }

    void CreateBackGround(float y)
    {
        Vector3 position = new Vector3(0f, y, 0f);

        GameObject bg = Instantiate(
             backgroudPrefab,
             position,
            Quaternion.identity);
        Vector3 scale = bg.transform.localScale;
        //ランダム左右反転
        if (Random.value < 0.5f)
        {
            scale.x *= -1f;
        }

        if (backgrounds.Count % 2 == 1)
        {
            scale.y *= -1f;
        }

        bg.transform.localScale = scale;

        backgrounds.Add(bg);

        lastY = y;
    }

    void DeleteOldBackgrounds()
    {
        for (int i = backgrounds.Count - 1; i >= 0; i--)
        {
            if (backgrounds[i] == null)
                continue;

            if (backgrounds[i].transform.position.y
                < player.position.y - deleteBelowDistance)
            {
                Destroy(backgrounds[i]);
                backgrounds.RemoveAt(i);
            }
        }
    }


}
