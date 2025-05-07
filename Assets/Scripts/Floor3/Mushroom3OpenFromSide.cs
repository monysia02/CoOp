using UnityEngine;

public class Mushroom3OpenFromSide : MonoBehaviour
{
    public Sprite pressedSprite;
    public Animator floorAnimator;

    private SpriteRenderer sr;
    private bool isActivated = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActivated) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactNormal = collision.contacts[0].normal;
            
            if (contactNormal.x > 0.5f)
            {
                ActivateSwitch(new Vector3(0.05f, 0, 0));
            }

            else if (contactNormal.x < -0.5f)
            {
                ActivateSwitch(new Vector3(-0.05f, 0, 0));
            }
        }
    }

    void ActivateSwitch(Vector3 offset)
    {
        isActivated = true;
        GetComponent<MushroomClickSound>()?.PlayClickSound();

        if (pressedSprite != null && sr != null)
            sr.sprite = pressedSprite;

        transform.position += offset;

        if (floorAnimator != null)
            floorAnimator.SetTrigger("platformOpen");
    }
}
