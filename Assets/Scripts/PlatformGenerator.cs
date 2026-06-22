
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    //生成する足場のPrefab
    public GameObject platformPrefab;

    //プレイヤーの位置を取得するためのtransform
    public Transform player;

    //ゲーム開始時に生成する足場の数
    public int startPlatformCount = 20;

    //最初の足場を生成するY座標
    public float startY = 2f;

    //足場同士の縦方向の間隔
    public float minYSpacing = 1.3f;
    public float maxYSpacing = 1.8f;

    //足場を生成するX座標の範囲
    public float minX = -7f;
    public float maxX = 7f;

    //足場同士の横方向の距離
    public float minXDistance = 4f;
    public float maxXDistance = 6f;

    //プレイヤーよりどれくらい先まで足場を生成するか
    public float generateAheadDistance = 20f;

    //プレイヤーより下に離れた足場を削除する距離
    public float deleteBelowDistance = 12f;

    //生成済みの足場を管理するリスト
    private List<GameObject> platforms = new List<GameObject>();

    //最後に生成した足場の位置
    private Vector3 lastPosition;

    //次に足場を生成する横方向
    private int nextDirection;

    void Start()
    {
        //最初の足場の位置を設定
        lastPosition = new Vector3(0f, startY, 0f);

        //スタート地点の足場を生成
        CreatePlatform(lastPosition);

        //最初に左右どちらへ足場を生成するかランダムに決定
        nextDirection = Random.value < 0.5f ? -1 : 1;

        //げーう開始時に一定数の足場を事前生成
        for (int i = 1; i < startPlatformCount; i++)
        {
            GenerateNextPlatform();
        }
    }

    void Update()
    {
        //プレイヤーの進行方向に足場が不足しないよう、先の足場を生成
        if (player.position.y + generateAheadDistance > lastPosition.y)
        {
            GenerateNextPlatform();
        }
        //プレイヤーから大きく下に離れた足場を削除
        DeleteOldPlatforms();
    }

    void GenerateNextPlatform()
    {
        //右端に近い場合あ次の足場を左方向に生成
        if (lastPosition.x > maxX - minXDistance)
        {
            nextDirection = -1;
        }
        //左端に近い場合は次の足場を右方向に生成
        else if (lastPosition.x < minX + minXDistance)
        {
            nextDirection = 1;
        }
        //横方向の距離をランダムに決定し、現在の生成方向を反映
        float xDistance = Random.Range(minXDistance, maxXDistance) * nextDirection;

        //次の足場のX座標を計算
        float x = lastPosition.x + xDistance;

        //足場が画面外に出ないようX座標を制限
        x = Mathf.Clamp(x, minX, maxX);

        //縦方向の間隔をランダムに決定
        float y = lastPosition.y + Random.Range(minYSpacing, maxYSpacing);

        //新しい足場の生成位置を設定
        Vector3 newPosition = new Vector3(x, y, 0f);

        //足場を生成
        CreatePlatform(newPosition);

        //最後に生成した足場の位置を更新
        lastPosition = newPosition;

        //次回は反対方向に芝を生成し、左右に動きのある配置にする
        nextDirection *= -1;
    }

    void CreatePlatform(Vector3 position)
    {
        //指定した位置に足場を生成
        GameObject newPlatform = Instantiate(platformPrefab, position, Quaternion.identity);

        //後で削除管理できるよう、生成した足場をリストに追加
        platforms.Add(newPlatform);
    }

    void DeleteOldPlatforms()
    {
        //リストの後ろから確認し、削除時のインデックスずれを防ぐ
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            //すでに削除されている足場は処理しない
            if (platforms[i] == null) continue;

            //プレイヤより一定距離下にある足場を削除
            if (platforms[i].transform.position.y < player.position.y - deleteBelowDistance)
            {
                Destroy(platforms[i]);
                //リストからも削除し、不要な参照を残さない
                platforms.RemoveAt(i);
            }
        }
    }
}