using UnityEngine;

public class FitBackgroundToCamera : MonoBehaviour
{
    void Start()
    {
        //ゲーム開始時に背景サイズを調節
        Fit();
    }

    void Fit()
    {
        //SpriteRendererを取得
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        //SpriteRenderが存在しない場合は処理しない
        if (sr == null) return;

        //背景反転情報を保持
        float signX = Mathf.Sign(transform.localScale.x);
        float signY = Mathf.Sign(transform.localScale.y);

        //カメラの表示領域サイズを取得
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;


        //スプライト本来のサイズを取得
        Vector2 spriteSize = sr.sprite.bounds.size;

        //横方向・縦方向の
        float scaleX = worldScreenWidth / spriteSize.x;
        float scaleY = worldScreenHeight / spriteSize.y;

        float scale = Mathf.Max(scaleX, scaleY);

        transform.localScale = new Vector3(
            scale * signX,
            scale * signY,
            1f
        );
    }
}