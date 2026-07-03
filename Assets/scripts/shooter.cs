
using UnityEngine;
using UnityEngine.InputSystem;

public class shooter : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite attackSprite;

    private SpriteRenderer spriteRenderer;

    public int state = 0;
    // 0: 待機
    // 1: 攻撃

    public float speed = 0f;
    public float rotation = 0f;
    public GameObject bulletPrefab;

    private Rigidbody2D rb;


    private void Awake(){
        
        // 画像を idleに
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite; 
        // Rigidbody2D のリファレンスを取得
        rb = GetComponent<Rigidbody2D>();
    }

    protected void ChangeState(int newState){
        state = newState;

        switch (state){
            case 0:
                spriteRenderer.sprite = idleSprite;
                break;
            case 1:
                spriteRenderer.sprite = attackSprite;
                break;
        }
    }

    protected void LookAt(Vector2 target)
    {
        // プレイヤーの位置とターゲットの位置の差を計算
        Vector2 diff = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
        // プレイヤーの右方向ベクトルを取得
        Vector2 rightDir = transform.right.normalized;
        // 内積を計算して、ターゲットが右側か左側かを判定
        float innerProduct = rightDir.x*diff.x + rightDir.y*diff.y;
        // 内積が正ならターゲットは右側、負なら左側
        if (innerProduct > 0)
        {
            transform.Rotate(0, 0, - rotation * Time.deltaTime);
        }
        if (innerProduct < 0)
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime);
        }
    }

    // 自身のローカル方向に合わせて移動する
    protected void Move(Vector2 dir)
    {
        var move = dir * speed;
        // 前方向との角度を計算して、速度ベクトルを回転させる
        float angle = Vector2.SignedAngle(new Vector2(0.0f,1.0f), transform.up.normalized);
        rb.velocity = Quaternion.Euler(0, 0, angle) * move;
    }

    protected void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab);

        Bullet bullet = obj.GetComponent<Bullet>();

        Vector2 pos = transform.position;
        Vector2 dir = transform.up; // プレイヤーの向いている方向

        bullet.Init(pos, dir, 8f);
        Destroy(obj, 3f); // 3秒後に弾を消す
    }
}