using UnityEngine;
using UnityEngine.InputSystem;

public class Enermy : shooter
{
    GameObject playerObj;
    void Start()
    {
        speed = 3f;
        rotation = 100f;
        playerObj = GameObject.FindWithTag("Player");
        
        ChangeState(0);
        // damageTag を設定
        damageTag = "bullet-friendly";
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 playerPosition = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        // 移動処理
        Move(new Vector2(0, 1));
        // shoot 処理
        // Debug.Log(transform.up.normalized);
        LookAt(playerPosition);
        if (state == 1)Shoot();
    }
}