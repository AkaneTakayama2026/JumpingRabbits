using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fallSpeed = 3f;
    public float deleteDistance = 12f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < player.position.y - deleteDistance)
        {
            Destroy(gameObject);
        }

    }
}
