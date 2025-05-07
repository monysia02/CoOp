using UnityEngine;

public class Mushroom4PressureSwitchSide : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite defaultSprite;

    public Animator floorAnimator;
    public Transform spriteHolder; 

    private SpriteRenderer sr;
    private Vector3 spriteOriginalPos;
    private bool isPressed = false;
    private Transform player;

    void Start()
    {
        sr = spriteHolder.GetComponent<SpriteRenderer>();
        spriteOriginalPos = spriteHolder.localPosition;
    }

    void Update()
    {
        if (player != null)
        {
            bool beside = Mathf.Abs(player.position.x - transform.position.x) < 0.5f;
            bool sameHeight = Mathf.Abs(player.position.y - transform.position.y) < 0.3f;

            if (beside && sameHeight && !isPressed)
            {
                Press();
            }
            else if ((!beside || !sameHeight) && isPressed)
            {
                Release();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player = collision.transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            Release();
        }
    }

    void Press()
    {
        isPressed = true;
        GetComponent<MushroomClickSound>()?.PlayClickSound();
        sr.sprite = pressedSprite;
        
        float direction = player.position.x - transform.position.x;

        if (direction > 0)
        {
            spriteHolder.localPosition = spriteOriginalPos + new Vector3(-0.03f, 0.1f, 0);
        }
        else
        {
            spriteHolder.localPosition = spriteOriginalPos + new Vector3(-0.03f, -0.1f, 0);
        }
        floorAnimator.SetBool("isOpen", true);
    }

    void Release()
    {
        isPressed = false;
        GetComponent<MushroomClickSound>()?.ResetClickSound();
        sr.sprite = defaultSprite;
        spriteHolder.localPosition = spriteOriginalPos;
        floorAnimator.SetBool("isOpen", false);
    }
}
