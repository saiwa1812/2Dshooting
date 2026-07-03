using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
        var move = new Vector2(0.0f, 1.0f) * speed;
        float angle = Vector2.SignedAngle(new Vector2(0.0f,1.0f), direction);
        rb.velocity = Quaternion.Euler(0, 0, angle) * move;
    }
}