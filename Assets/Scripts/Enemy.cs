using UnityEngine;

public class Enemy : MonoBehaviour
{
    //敵が落下する速度
    public float fallSpeed = 3f;

    //プレイヤから一定距離下に離れた敵を削除する距離
    public float deleteDistance = 12f;

    //敵撃破時に加算するスコア
    public int scoreValue = 10;

    //プレイヤの位置を取得するためのTransform
    private Transform player;

    private void Start()
    {
        //Playerタグを持つオブジェクトを検索し、Transformを取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //敵を一定速度で下方向へ移動させる
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        //プレイヤより一定距離下に移動した敵を削除する
        //不要なオブジェクトを削除し、パフォーマンス低下を防ぐ
        if (transform.position.y < player.position.y - deleteDistance)
        {
            Destroy(gameObject);
        }

    }
}
