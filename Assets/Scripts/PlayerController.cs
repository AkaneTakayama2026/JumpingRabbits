using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public GameObject healParticlePrefab;
    public float invincibleTime = 1f;
    private bool isInvincible = false;
    public bool canAirJump = false;
    public GameObject mochiBeamPrefab;
    public Transform attackPoint;

    public int life = 3;
    public TextMeshProUGUI lifeText;

    private Vector3 lastSafePosition;
    private bool isGameOver = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //プレイヤーの移動速度
    public float moveSpeed = 5f;
    //ジャンプ力
    public float jumpPower = 15f;

    private Rigidbody2D rb;
    //地面に接しているかどうか
    private bool isGrounded = false;



    void Start()
    {
        //rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        lastSafePosition = transform.position;
        UpdateLifeUI();

    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);

        //左右移動入力
        float moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Attack();
        }

        //左右移動を適用
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //地面にいて、スペースキーが押されたらジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame && (isGrounded || canAirJump))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            //空中状態に変更
            isGrounded = false;
        }

        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        CheckGameOver();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Platformに接触したら着地状態にする
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            lastSafePosition = transform.position;
        }
    }

    void GameOver()
    {
        SceneData.lastPlaySceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameOverScene");
    }


    void CheckGameOver()
    {
        if (isGameOver) return;

        float cameraY = Camera.main.transform.position.y;

        if (transform.position.y < cameraY - 5f)
        {
            Miss();
        }
    }

    void Attack()
    {
        Vector2 direction = Vector2.up;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            direction = Vector2.left;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            direction = Vector2.right;
        }
        GameObject beam = Instantiate(mochiBeamPrefab, attackPoint.position, Quaternion.identity);

        Mochibeam mochiBeam = beam.GetComponent<Mochibeam>();

        if (mochiBeam != null)
        {
            mochiBeam.setDirection(direction);
        }


    }

    void Heal()
    {
        if (life < 3)
        {
            life++;

            UpdateLifeUI();
        }
    }

    void Miss()
    {
        life--;
        UpdateLifeUI();

        if (life <= 0)
        {
            isGameOver = true;
            GameOver();
            return;
        }
        rb.linearVelocity = Vector2.zero;
        transform.position = lastSafePosition + new Vector3(0f, 1f, 0f);
    }
    void UpdateLifeUI()
    {
        string hearts = "";

        for (int i = 0; i < life; i++)
        {
            hearts += "♥";
        }
        lifeText.text = hearts;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (isInvincible) return;
            Destroy(collision.gameObject);
            Miss();

            StartCoroutine(InvincibleCoroutine());
        }
        if (collision.CompareTag("Goal"))
        {
            SceneManager.LoadScene("2ndStageScene");
        }
        if (collision.CompareTag("RecoveryItem"))
        {
            Heal();

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("RecoveryItem"))
        {
            if (healParticlePrefab != null)
            {
                Instantiate(healParticlePrefab, collision.transform.position, Quaternion.identity);
            }
            Heal();
            Destroy(collision.gameObject);
        }
        System.Collections.IEnumerator InvincibleCoroutine()
        {
            isInvincible = true;
            yield return new WaitForSeconds(invincibleTime);

            isInvincible = false;
        }
    }

}
