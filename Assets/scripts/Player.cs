using UnityEngine;
using UnityEngine.InputSystem;

public class Player : shooter
{
    void Awake()
    {
        // "Move" と "Shoot" のリファレンスを探す
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        lookAction = InputSystem.actions.FindAction("Look");
        speed = 10f;
        rotation = 200f;
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
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(pointerScreenPosition);

        LookAtPointer(pointerPosition);
        if (state == 1)Shoot();
    }
}
