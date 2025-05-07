using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public string disappearAnimationTrigger = "disappear";

    private Rigidbody2D rb;
    private bool hasHit = false;
    private GameObject targetWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb.linearVelocity == Vector2.zero)
            rb.linearVelocity = transform.right * speed;
    }

    public void SetTarget(GameObject target)
    {
        targetWall = target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (hasHit) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            var deathHandler = collision.gameObject.GetComponent<CharacterDeathHandler>();
            if (deathHandler != null)
            {
                deathHandler.TriggerDeath(); 
            }

            HitAndDestroy();
            return;
        }
        
        if (collision.gameObject.name == targetWall.name)
        {
            HitAndDestroy();
        }
    }

    void HitAndDestroy()
    {
        hasHit = true;
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        if (animator != null)
            animator.SetTrigger(disappearAnimationTrigger);
        else
            Destroy(gameObject);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
