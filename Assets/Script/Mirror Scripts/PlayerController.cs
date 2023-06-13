using Mirror;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NetworkAnimator networkAnimator;

    public float speed = 20f;
    public float jumpForce;
    public float maxDistance;
    public LayerMask layerMask;
    public RectTransform HealthBar;

    private Rigidbody2D rb;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            // Disable the Rigidbody2D component on non-local players
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = false;
            }
            return;
        }

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
        if (!isLocalPlayer) { return; }

        Vector2 newSpeed = rb.velocity;
        Vector2 newScale = transform.localScale;
        Vector3 HealthBarScale = HealthBar.transform.localScale;

        UpdateAnimations(newSpeed, newScale, HealthBarScale);

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

    private void UpdateAnimations(Vector2 newSpeed, Vector2 newScale, Vector3 HealthBarScale)
    {
        newSpeed.x = Input.GetAxisRaw("Horizontal") * speed;
        // Running animation
        animator.SetFloat("Speed", Mathf.Abs(newSpeed.x));

        newSpeed.y = rb.velocity.y;
        rb.velocity = newSpeed;
        //Jumping animation
        if (!IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
        animator.SetFloat("yVelocity", rb.velocity.y);

        // Change facing direction when turning
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
    [ClientRpc]
    private void RpcUpdateAnimation(bool isAnimating)
    {
        // Update animation parameters on all clients except the local player
        if (!isLocalPlayer && networkAnimator != null)
        {
            networkAnimator.animator.SetBool("IsAnimating", isAnimating);
            // UpdateAnimations(newSpeed, newScale, HealthBarScale);
        }
    }
}
