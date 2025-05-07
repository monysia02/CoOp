using UnityEngine;

public class Mushroom1PressureSwitch : MonoBehaviour
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
            bool onTop = player.position.y > transform.position.y + 0.2f &&
                         Mathf.Abs(player.position.x - transform.position.x) < 0.5f;

            if (onTop && !isPressed)
                Press();
            else if (!onTop && isPressed)
                Release();
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
        spriteHolder.localPosition = spriteOriginalPos + new Vector3(0, -0.1f, 0);
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
