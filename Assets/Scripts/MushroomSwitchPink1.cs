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

        //Gracz?
        if (collision.gameObject.CompareTag("Player"))
        {
            //Czy z góry?
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

        if (pressedSprite != null && sr != null)
            sr.sprite = pressedSprite;
        
        if (cageAnimator != null)
            cageAnimator.SetTrigger("disappear");
        
        //Wyłączenie kolizji
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
