using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public BallColor requiredColor; 
    public SpriteRenderer buttonSpriteRenderer;
    public Sprite pressedSprite;
    public List<Animator> platformsToRise;

    private bool isActivated = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated) return;
        
        ColorBall ball = other.GetComponent<ColorBall>();

        if (ball != null && ball.ballColor == requiredColor)
        {
            isActivated = true;
            
            foreach (Animator anim in platformsToRise)
            {
                if (anim != null)
                    anim.SetTrigger("appear");
            }

            if (buttonSpriteRenderer != null && pressedSprite != null)
            {
                buttonSpriteRenderer.sprite = pressedSprite;
                transform.position += new Vector3(0f, -0.05f, 0f);
                GetComponent<MushroomClickSound>()?.PlayClickSound();
            }
        }
    }
}
