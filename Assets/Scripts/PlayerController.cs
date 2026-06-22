using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //攻撃可能かどうかを管理する
    public bool canAttack = false;

    //回復・ジャンプ時に再生する効果音
    public AudioClip healSE;
    public AudioClip jumpSE;

    //効果音を再生するためのAudioSource
    private AudioSource audioSource;

    //回復アイテム取得時に表示するパーティクル
    public GameObject healParticlePrefab;

    //ダメージ後の無敵時間
    public float invincibleTime = 1f;
    private bool isInvincible = false;

    //空中ジャンプが可能かを管理
    public bool canAirJump = false;

    //攻撃時に生成する弾と発射位置
    public GameObject mochiBeamPrefab;
    public Transform attackPoint;

    //プレイヤーの残りライフ
    public int life = 3;
    //ライフ表示用UI
    public TextMeshProUGUI lifeText;

    //落下時に戻るための最後に安全だった位置
    private Vector3 lastSafePosition;

    //ゲームオーバー処理の重複実行を防ぐフラグ
    private bool isGameOver = false;

    //プレイヤーの見た目やアニメーションを制御するコンポーネント
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //プレイヤーの移動速度
    public float moveSpeed = 5f;
    //ジャンプ力
    public float jumpPower = 15f;


    //物理演算による移動制御に使用
    private Rigidbody2D rb;

    //地面に接しているかどうか
    private bool isGrounded = false;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        //初期位置を安全な位置として保存
        lastSafePosition = transform.position;

        //ゲーム開始時にライフUIを更新
        UpdateLifeUI();


    }

    private void Update()
    {
        //移動速度と設置状態をAnimatorへ渡し、アニメーションを切り替える
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);

        //左右移動の入力値を初期化
        float moveInput = 0f;

        //Aキー・左矢印キーで移動
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }

        //Dキー・右矢印キーで移動
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }

        //攻撃可能な状態でBキーが押された場合、攻撃処理を実行
        if (canAttack && Keyboard.current.bKey.wasPressedThisFrame)
        {
            Attack();
        }

        //入力値に応じてプレイヤーを左右移動させる
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //地面にいるか、空中ジャンプ可能な状態でスペースキーが押されたらジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame && (isGrounded || canAirJump))
        {
            audioSource.PlayOneShot(jumpSE, SEManager.seVolume);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            //空中状態に変更
            isGrounded = false;
        }

        //移動方向に応じてプレイヤーの向きを反転
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        //画面外へ落下していないか確認
        CheckGameOver();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Platformタグのオブジェクトに接触したら着地状態にする
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;

            //落下時に復帰できるよう、安全な位置を保存
            lastSafePosition = transform.position;
        }
    }

    void GameOver()
    {
        //ゲームオーバー時にハイスコアを保存
        ScoreManager.SaveHighScore();

        //直前に遊んでいたシーン名を保存し、ゲームオーバー画面へ遷移
        SceneData.lastPlaySceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameOverScene");
    }


    void CheckGameOver()
    {
        //すでにゲームオーバーの場合は処理を行わない
        if (isGameOver) return;

        //カメラ位置より一定以上下に落下した場合、ミス扱いにする
        float cameraY = Camera.main.transform.position.y;

        if (transform.position.y < cameraY - 5f)
        {
            Miss();
        }
    }

    void Attack()
    {
        //初期状態では上方向に攻撃する
        Vector2 direction = Vector2.up;

        //入力方向に応じて攻撃方向を変更
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            direction = Vector2.left;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            direction = Vector2.right;
        }

        //攻撃用の弾を生成
        GameObject beam = Instantiate(mochiBeamPrefab, attackPoint.position, Quaternion.identity);

        //生成した弾に攻撃方向を設定
        Mochibeam mochiBeam = beam.GetComponent<Mochibeam>();

        if (mochiBeam != null)
        {
            mochiBeam.setDirection(direction);
        }


    }

    void Heal()
    {
        //ライフが最大値未満の場合のみ回復
        if (life < 3)
        {
            life++;

            //回復効果音を再生
            audioSource.PlayOneShot(healSE, SEManager.seVolume);

            //回復後のライフをUIに反映
            UpdateLifeUI();
        }
    }

    void Miss()
    {
        //ミスしたためライフを１減らす
        life--;
        UpdateLifeUI();

        //ライフが０になった場合、ゲームオーバー処理を実行
        if (life <= 0)
        {
            isGameOver = true;
            GameOver();
            return;
        }

        //落下や被ダメージ後、速度をリセットして安全な位置に戻す
        rb.linearVelocity = Vector2.zero;
        transform.position = lastSafePosition + new Vector3(0f, 1f, 0f);
    }
    void UpdateLifeUI()
    {
        //ライフ数に応じてハートを表示
        string hearts = "";

        for (int i = 0; i < life; i++)
        {
            hearts += "♥";
        }
        lifeText.text = hearts;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵に接触した場合の処理
        if (collision.CompareTag("Enemy"))
        {
            //無敵時間中はダメージを受けない
            if (isInvincible) return;

            //敵を消してライフを減らす
            Destroy(collision.gameObject);
            Miss();

            //ダメージ後に一定時間無敵状態にする
            StartCoroutine(InvincibleCoroutine());
        }

        //ゴールに到達したら次のステージへ遷移
        if (collision.CompareTag("Goal"))
        {
            SceneManager.LoadScene("2ndStageScene");
        }


        //回復アイテムを取得した場合の処理
        if (collision.CompareTag("RecoveryItem"))
        {
            if (healParticlePrefab != null)
            {
                Instantiate(healParticlePrefab, collision.transform.position, Quaternion.identity);
            }
            //ライフを回復し、取得したアイテムを削除
            Heal();
            Destroy(collision.gameObject);
        }
        System.Collections.IEnumerator InvincibleCoroutine()
        {
            //一定時間ダメージを受けない状態にする
            isInvincible = true;
            yield return new WaitForSeconds(invincibleTime);

            //無敵時間終了後、通常状態に戻す
            isInvincible = false;
        }
    }

}
