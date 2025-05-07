using System.Collections;
using UnityEngine;

public class MushroomSwitchPink1 : MonoBehaviour
{
    public Sprite pressedSprite;  
    private bool isActivated = false;
    private SpriteRenderer sr;
    public Animator cageAnimator;
    public GameObject cage;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActivated) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                ActivateSwitch();
                transform.position += new Vector3(0, -0.1f, 0);
            }
        }
    }
    
    void ActivateSwitch()
    {
        isActivated = true;
        GetComponent<MushroomClickSound>()?.PlayClickSound();

        if (pressedSprite != null && sr != null)
            sr.sprite = pressedSprite;
        
        if (cageAnimator != null)
            cageAnimator.SetTrigger("disappear");
        
        if (cage != null)
        {
            BoxCollider2D[] colliders = cage.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D col in colliders)
            {
                col.enabled = false;
            }
        }
    }
}
