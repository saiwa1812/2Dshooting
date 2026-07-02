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
    private InputAction moveAction;
    private InputAction shootAction;


    public void awake(){
        
    }

    private void Start(){
        // "Move" と "Shoot" のリファレンスを探す
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        // 画像を idleに
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite; 
    }

    public void ChangeState(int newState){
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

    private void Update()
    {
        // 移動処理
        var moveValue = moveAction.ReadValue<Vector2>();
        var move = new Vector2(moveValue.x, moveValue.y) * speed * Time.deltaTime;
        transform.Translate(move);
        // ジャンプ処理
        Debug.Log("shoot is null");
        if (shootAction.IsPressed()){
            ChangeState(1);
        }
        else
        {
            ChangeState(0);
        }   
    }
}