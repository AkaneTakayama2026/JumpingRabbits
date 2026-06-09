using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの移動速度
    public float moveSpeed = 5f;
    //ジャンプ力
    public float jumpPower = 10f;

    private Rigidbody2D rb;
    //地面に接しているかどうか
    private bool isGrounded = false;
    private bool isGameOver = false;


    void Start()
    {
        //rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
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
        //左右移動を適用
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //地面にいて、スペースキーが押されたらジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            //空中状態に変更
            isGrounded = false;
        }

        CheckGameOver();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Platformに接触したら着地状態にする
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
    void CheckGameOver()
    {
        if (isGameOver) return;

        float cameraY = Camera.main.transform.position.y;

        if (transform.position.y < cameraY - 7f)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
    }

}
