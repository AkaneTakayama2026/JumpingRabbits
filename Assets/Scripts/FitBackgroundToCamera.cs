using UnityEngine;

public class FitBackgroundToCamera : MonoBehaviour
{
    void Start()
    {
        Fit();
    }

    void Fit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null) return;

        float signX = Mathf.Sign(transform.localScale.x);
        float signY = Mathf.Sign(transform.localScale.y);

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

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