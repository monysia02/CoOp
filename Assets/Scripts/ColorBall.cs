using UnityEngine;

public class ColorBall : MonoBehaviour
{
    public BallColor ballColor;
    
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isTouchingGround = false;

    [SerializeField] private float minSpeedToPlay = 0.2f;
    
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float impactVolume = 0.9f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float speed = rb.linearVelocity.magnitude;

        if (isTouchingGround && speed > minSpeedToPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
            
            audioSource.volume = Mathf.Clamp01(speed / 5f);
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isTouchingGround = true;
        if (collision.gameObject.CompareTag("Button"))
        {
            if (impactSound != null)
                AudioSource.PlayClipAtPoint(impactSound, transform.position, impactVolume);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isTouchingGround = false;
    }
}
