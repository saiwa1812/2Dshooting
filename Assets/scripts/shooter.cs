using UnityEngine;

public class shooter : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite attackSprite;

    private SpriteRenderer spriteRenderer;

    public int state = 0;
    // 0: 待機
    // 1: 攻撃

    public void awake(){
        spriteRenderer.sprite = idleSprite;
    }

    public void ChangeState(int newState){
        state = newState;

        switch (state)
        {
            case 0:
                spriteRenderer.sprite = idleSprite;
                break;
            case 1:
                spriteRenderer.sprite = attackSprite;
                break;
        }
    }
}