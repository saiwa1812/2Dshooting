using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    // 初期化関数
    public void Init(Vector2 startPos, Vector2 dir, float spd)
    {
        transform.position = startPos;
        direction = dir.normalized;
        speed = spd;
        transform.up = direction;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (rb == null)
        {
            return;
        }

        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other.gameObject);
    }


    private void HandleHit(GameObject hitObject)
    {
        if (hitObject == gameObject)
        {
            return;
        }

        var shooter = hitObject.GetComponentInParent<shooter>();
        if (shooter != null && hitObject.CompareTag("Enermy"))
        {
            // Debug.Log(hitObject.name + " に当たりました");
            Destroy(gameObject);
        }
    }
}