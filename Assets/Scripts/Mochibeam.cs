using UnityEngine;

public class Mochibeam : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 3f;
    public GameObject explosionPrefab;

    private Vector2 moveDirection = Vector2.up;

    public void setDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
