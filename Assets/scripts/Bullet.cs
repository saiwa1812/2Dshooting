using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    // 初期化関数
    public void Init(Vector2 startPos, Vector2 dir, float spd)
    {
        transform.position = startPos;
        direction = dir.normalized;
        speed = spd;
        transform.up = direction;
    }

    void Update()
    {  
        var move = new Vector2(0.0f, 1.0f) * speed * Time.deltaTime;
        transform.Translate(move);
    }
}