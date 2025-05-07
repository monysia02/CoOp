using System.Collections;
using UnityEngine;

public class CharacterDeathHandler : MonoBehaviour
{
    public enum PlayerType { Ladybug, Cat }
    public PlayerType playerType;
    public string idleAnimationName = "LadybugIdle"; //lub CatIdle
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private bool isDead = false;
    
    public int maxLives = 3;
    private int currentLives;

    [SerializeField] private LifeDisplay lifeDisplay;
    
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        currentLives = maxLives;
        audioSource = GetComponent<AudioSource>();

    }

    public void TriggerDeath()
    {
        audioSource?.Stop();
        AdditionalSoundsManager.Instance.PlayDamage(damageSound);
        
        if (isDead || currentLives <= 0) return;

        currentLives--;

        lifeDisplay?.UpdateLives(currentLives);
        
        isDead = true;
        if (animator != null)
        {
            animator.SetBool("run", false);
            animator.SetBool("grounded", false);
            animator.Play(idleAnimationName);  
        }
        StartCoroutine(DeathSequence());
    }
    
    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator DeathSequence()
    {
        body.linearVelocity = Vector2.zero;
        body.gravityScale = 0;
        body.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;

        if (animator != null)
        {
            animator.Play(idleAnimationName);
            yield return null;
        }

        yield return MoveByOffset(new Vector2(0, -0.5f), 0.2f);
        transform.position += new Vector3(0f, 0.7f, 0f);

        if (spriteRenderer != null) spriteRenderer.sortingOrder = 100;

        if (spriteRenderer != null)
        {
            for (int i = 0; i < 3; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, 90);
        body.bodyType = RigidbodyType2D.Dynamic;
        body.gravityScale = 1f;

        yield return StartCoroutine(WaitUntilY(-4f));

        if (playerType == PlayerType.Ladybug)
            GameManager.Instance.CheckGameOver(currentLives, FindLadybugLives());
        else if (playerType == PlayerType.Cat)
            GameManager.Instance.CheckGameOver(FindCatLives(), currentLives);
        
        if (currentLives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }


    private IEnumerator MoveByOffset(Vector2 offset, float duration)
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)offset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
    
    private IEnumerator WaitUntilY(float minY)
    {
        while (transform.position.y > minY)
        {
            yield return null; 
        }
        
        Vector3 pos = transform.position;
        pos.y = minY;
        transform.position = pos;

        body.linearVelocity = Vector2.zero;
        body.gravityScale = 0;
        body.bodyType = RigidbodyType2D.Kinematic;
    }
    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 respawnPos;

        if (TryGetComponent<LadyBugController>(out _))
        {
            respawnPos = CheckpointManager.Instance.GetRespawnPosition_Ladybug();
        }
        else if (TryGetComponent<CatPlayerController>(out _))
        {
            respawnPos = CheckpointManager.Instance.GetRespawnPosition_Cat();
        }
        else
        {
            respawnPos = transform.position;
        }
        
        transform.rotation = Quaternion.identity;
        transform.position = respawnPos;
        
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        body.bodyType = RigidbodyType2D.Dynamic;
        body.gravityScale = 1f;
        col.enabled = true;

        animator.Play(idleAnimationName);
        isDead = false;
        
        spriteRenderer.sortingOrder = 0;
        StartCoroutine(FadeIn(0.5f));
        GetComponent<KillZoneChecker>()?.ResetDiedFlag();

    }
    
    private IEnumerator FadeIn(float duration)
    {
        float elapsed = 0f;
        Color c = spriteRenderer.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(c.r, c.g, c.b, 1f);
    }
    
    private int FindLadybugLives()
    {
        var other = FindFirstObjectByType<LadyBugController>()?.GetComponent<CharacterDeathHandler>();
        return other != null ? other.currentLives : 0;
    }

    private int FindCatLives()
    {
        var other = FindFirstObjectByType<CatPlayerController>()?.GetComponent<CharacterDeathHandler>();
        return other != null ? other.currentLives : 0;
    }

}