using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x > 7f)
        {
            direction = -1;
        }

        if (transform.position.x < -7f)
        {
            direction = 1;
        }

    }
}
