using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SimpleMovement : NetworkBehaviour 
{
    public float speed = 20f;
    public float jumpForce;
    public float maxDistance;
    public LayerMask layerMask;
    public RectTransform HealthBar;
    private Rigidbody2D rb;
    Animator animator;
    public bool isRabbit;
    public bool faceRight;

    public void SetRabbit() { isRabbit = true; Debug.Log("set Rabbit"); }
    public void SetFox() { isRabbit = false; Debug.Log("set Fox"); }

    void Start()
    {
        if (!isLocalPlayer) { return; }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (isRabbit)
        {
            animator.SetBool("isRabbit", true); Debug.Log("Rabbit Animator select");
        }
        else
        {
            animator.SetBool("isRabbit", false); Debug.Log("Fox Animator select");
        }
    }

    void Update()
    {
        if (!isLocalPlayer) { return; }
        // jump action
        if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
                //Debug.Log("Jumped\nbecause isGrounded is " + IsGrounded());
            }   
    }

    public void FixedUpdate() 
    {
        if (!isLocalPlayer) { return; }
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
        if (Input.GetAxis("Horizontal") < 0) // facing left
        {
            newScale.x = -1;
            HealthBarScale.x = -1;
            faceRight = false;
        }
        else if (Input.GetAxis("Horizontal") > 0) // facing right
        {
            newScale.x = 1;               
            HealthBarScale.x = 1;
            faceRight = true;
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
