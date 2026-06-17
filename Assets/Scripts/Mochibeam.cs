using UnityEngine;

public class Mochibeam : MonoBehaviour
{
    public AudioClip hitSE;
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ビームが何かに当たった: " + collision.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemyに命中");
            EnemyData enemyData = collision.GetComponent<EnemyData>();

            if (enemyData != null)
            {
                ScoreManager.AddScore(enemyData.scoreValue);
            }
            else
            {
                Debug.LogWarning("EnemyDataがついてません");
            }

            GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            AudioSource explosionAudio = explosion.GetComponent<AudioSource>();

            if (explosionAudio != null)
            {
                explosionAudio.volume = SEManager.seVolume;
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
