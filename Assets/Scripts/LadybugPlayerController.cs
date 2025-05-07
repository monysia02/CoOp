using System;
using System.Collections;
using UnityEngine;

public class LadyBugController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip runLoopSound;
    
    private AudioSource audioSource;
    private bool isRunningSoundPlaying = false;
    
    private CharacterDeathHandler deathHandler;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        deathHandler = GetComponent<CharacterDeathHandler>();
    }

    private void Update()
    {
        if (deathHandler != null && deathHandler.IsDead())
            return;
        
        float horizontalInput = Input.GetAxis("Horizontal_Ladybug");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.W) && grounded)
            Jump();
        
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", grounded);
        
        if (Mathf.Abs(horizontalInput) > 0.1f && grounded)
        {
            if (!isRunningSoundPlaying)
            {
                AdditionalSoundsManager.Instance.PlayRunLoop(runLoopSound);
                isRunningSoundPlaying = true;
            }
        }
        else
        {
            if (isRunningSoundPlaying)
            {
                AdditionalSoundsManager.Instance.StopRunLoop();
                isRunningSoundPlaying = false;
            }
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
        if (jumpSound != null)
            AdditionalSoundsManager.Instance.PlayJump(jumpSound);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    

}