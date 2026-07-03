using UnityEngine;
using UnityEngine.InputSystem;

public class Enermy : shooter
{
    GameObject playerObj;
    void Start()
    {
        // "Move" と "Shoot" のリファレンスを探す
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        lookAction = InputSystem.actions.FindAction("Look");
        speed = 3f;
        rotation = 100f;
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 playerPosition = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        // 移動処理
        Move(transform.up.normalized);
        // shoot 処理
        if (shootAction.IsPressed()){
            ChangeState(1);
        }
        else
        {
            ChangeState(0);
        }   
        LookAt(playerPosition);
        if (state == 1)Shoot();
    }
}