using UnityEngine;

public class Mochibeam : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 3f;
    public GameObject explosionPrefab;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
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
