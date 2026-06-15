using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    //上下移動の速さ
    public float floatSpeed = 2f;
    //上下移動の幅
    public float floatHeight = 0.2f;
    //初期位置を保存
    private Vector3 startPosition;
    //UIオブジェクト用のRectTransform
    private RectTransform rectTransform;
    //UIオブジェクトかどうかを判定するフラグ
    private bool isUI = false;
    void Start()
    {
        //RectTransformを取得
        rectTransform = GetComponent<RectTransform>();

        //UIオブジェクトの場合
        if (rectTransform != null)
        {
            isUI = true;

            //UI座標を初期位置として保存
            startPosition = rectTransform.anchoredPosition;
        }
        else
        {
            //ワールド座標を初期位置として保存
            startPosition = transform.position;
        }
    }

    void Update()
    {
        //Sin波を利用して上下に揺れる値を計算
        float y =
            Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        //UIオブジェクトの場合
        if (isUI)
        {
            rectTransform.anchoredPosition =
                startPosition + new Vector3(0, y, 0);
        }
        //通常のゲームオブジェクトの場合
        else
        {
            transform.position =
                startPosition + new Vector3(0, y, 0);
        }
    }
}
