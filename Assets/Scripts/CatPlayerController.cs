using System;
using Unity.VisualScripting;
using UnityEngine;

public class CatPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal_Cat");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); //bez linear

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one; 
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1); 
        
        if(Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();
        
        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
}