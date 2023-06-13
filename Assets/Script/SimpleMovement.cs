using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class SimpleMovement : NetworkBehaviour 
{
    public float speed = 20f;
    public float jumpForce;
    public float maxDistance;
    public LayerMask layerMask;
    public RectTransform HealthBar;
    private Rigidbody2D rb;
    Animator animator;


    void Start()
    {
        if (!isLocalPlayer) return;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (!isLocalPlayer) return;
        // jump action
        if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
                //Debug.Log("Jumped\nbecause isGrounded is " + IsGrounded());
            }   
    }

    private void FixedUpdate() 
    {
            if (!isLocalPlayer) return;
            // horizontal move
            // alternative way
            /*float newSpeed = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            animator.SetFloat("Speed", Mathf.Abs(newSpeed));
            transform.Translate(newSpeed, 0f, 0f);*/
            Vector2 newSpeed;

            newSpeed.x = Input.GetAxisRaw("Horizontal") * speed; //Debug.Log(newSpeed.x);
            animator.SetFloat("Speed", Mathf.Abs(newSpeed.x));

            newSpeed.y = rb.velocity.y;
            rb.velocity = newSpeed;

            // update animation
            if (!IsGrounded())
            {
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
            animator.SetFloat("yVelocity", rb.velocity.y);

            Vector2 newScale = transform.localScale;
            Vector3 HealthBarScale = HealthBar.transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                newScale.x = -1;
                HealthBarScale.x = -1;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                newScale.x = 1;               
                HealthBarScale.x = 1; 
            }
            transform.localScale = newScale;
            HealthBar.transform.localScale = HealthBarScale;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -(maxDistance * transform.up));
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, maxDistance, layerMask).collider;
    }
}
