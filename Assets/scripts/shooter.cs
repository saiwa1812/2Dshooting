
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

    private float speed = 10f;
    private float rotation = 200f;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction lookAction;
    public GameObject bulletPrefab;

    private Rigidbody2D rb;

    void Awake()
    {

    }

    private void Start(){
        // "Move" と "Shoot" のリファレンスを探す
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        lookAction = InputSystem.actions.FindAction("Look");
        // 画像を idleに
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite; 

        rb = GetComponent<Rigidbody2D>();
    }

    private void ChangeState(int newState){
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

    private void LookAtPointer()
    {
        Vector2 shooterPosition = new Vector2(transform.position.x,transform.position.y);
        Vector2 pointerScreenPosition = lookAction.ReadValue<Vector2>();
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(pointerScreenPosition);
        Vector2 diff = (pointerPosition - shooterPosition).normalized;

        Vector2 rightDir = transform.right.normalized;


        float innerProduct = rightDir.x*diff.x + rightDir.y*diff.y;

        if (innerProduct > 0)
        {
            transform.Rotate(0, 0, - rotation * Time.deltaTime);
        }
        if (innerProduct < 0)
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime);
        }
        
    }

    private void Move()
    {
        var moveValue = moveAction.ReadValue<Vector2>();
        var move = new Vector2(moveValue.x, moveValue.y) * speed;

        float angle = Vector2.SignedAngle(new Vector2(0.0f,1.0f), transform.up.normalized);

        rb.linearVelocity = Quaternion.Euler(0, 0, angle) * move;
    }

    private void Update()
    {
        // 移動処理
        Move();
        // shoot 処理
        if (shootAction.IsPressed()){
            ChangeState(1);
        }
        else
        {
            ChangeState(0);
        }   

        LookAtPointer();
        if (state == 1)Shoot();
    }


    void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab);

        Bullet bullet = obj.GetComponent<Bullet>();

        Vector2 pos = transform.position;
        Vector2 dir = transform.up; // プレイヤーの向いている方向

        bullet.Init(pos, dir, 200f);
        Destroy(obj, 3f); // 3秒後に弾を消す
    }
}