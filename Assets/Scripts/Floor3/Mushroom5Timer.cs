using UnityEngine;

public class Mushroom5Timer : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite defaultSprite;

    public Animator floorAnimator;
    public Transform spriteHolder;

    public float delayAfterExit = 5.0f;

    private SpriteRenderer sr;
    private Vector3 spriteOriginalPos;

    private bool isPressed = false;
    private float exitTimer = 0f;
    private bool playerIsOn = false;
    private Transform player;

    void Start()
    {
        sr = spriteHolder.GetComponent<SpriteRenderer>();
        spriteOriginalPos = spriteHolder.localPosition;
    }

    void Update()
    {
        if (playerIsOn && !isPressed)
        {
            Press();
        }
        else if (!playerIsOn && isPressed)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer >= delayAfterExit)
            {
                Release();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
            playerIsOn = true;
            exitTimer = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            playerIsOn = false;
            exitTimer = 0f;
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
