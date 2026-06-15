using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    public float moveSpeed = 1f;

    private void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}
