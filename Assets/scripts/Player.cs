using UnityEngine;
using UnityEngine.InputSystem;

public class Player : shooter
{
    protected InputAction moveAction;
    protected InputAction shootAction;
    protected InputAction lookAction;


    void Start()
    {
        // "Move" と "Shoot" のリファレンスを探す
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        lookAction = InputSystem.actions.FindAction("Look");
        speed = 10f;
        rotation = 200f;
        // damageTag を設定
        damageTag = "bullet-enermy";
        self = gameObject;

        hitPoint = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        // 移動処理
        Move(moveAction.ReadValue<Vector2>());
        // shoot 処理
        if (shootAction.IsPressed()){
            ChangeState(1);
        }
        else
        {
            ChangeState(0);
        }   

        Vector2 pointerScreenPosition = lookAction.ReadValue<Vector2>();
        // スクリーン座標をワールド座標に変換
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(pointerScreenPosition);

        LookAt(pointerPosition);
        if (state == 1)Shoot();
    }
}
