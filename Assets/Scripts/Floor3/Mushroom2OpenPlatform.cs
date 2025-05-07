using UnityEngine;

public class Mushroom2OpenPlatform : MonoBehaviour
{
    public Sprite pressedSprite;  
    public GameObject floorToOpen;
    private bool isActivated = false;
    private SpriteRenderer sr;
    public Animator floorAnimator;

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
        GetComponent<MushroomClickSound>()?.PlayClickSound();

        if (pressedSprite != null && sr != null)
            sr.sprite = pressedSprite;

        if (floorAnimator != null)
            floorAnimator.SetTrigger("platformOpen");
    
    }
}
