using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public GameObject wallTarget;

    public float arrowSpeed = 5f;
    public bool shootRight = true;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float audibleLeft = 5f;
    [SerializeField] private float audibleRight = 5f;
    [SerializeField] private float audibleUp = 4f;
    [SerializeField] private float audibleDown = 4f;

    [SerializeField] private float minShootInterval = 3f;
    [SerializeField] private float maxShootInterval = 7f;

    private float shootInterval;
    private float timer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = Random.Range(0f, minShootInterval); 
        shootInterval = Random.Range(minShootInterval, maxShootInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            ShootArrow();
            timer = 0f;
            shootInterval = Random.Range(minShootInterval, maxShootInterval);
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, Quaternion.identity);

        if (!shootRight)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        if (playerTransform != null)
        {
            Vector2 delta = playerTransform.position - spawnPoint.position;

            bool withinX = delta.x >= -audibleLeft && delta.x <= audibleRight;
            bool withinY = delta.y >= -audibleDown && delta.y <= audibleUp;

            if (withinX && withinY)
            {
                audioSource?.Play();
            }
        }

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = shootRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * arrowSpeed, 0f);
        }

        ArrowProjectile arrowScript = arrow.GetComponent<ArrowProjectile>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(wallTarget);
        }
    }
}
