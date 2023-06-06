using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce;
    public float maxDistance;
    public LayerMask layerMask;

    private Rigidbody2D rb;
    Animator animator;
    PhotonView view;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            // jump action
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
                //Debug.Log("Jumped\nbecause isGrounded is " + IsGrounded());
            }
        }
    }

    private void FixedUpdate() 
    {
        if (view.IsMine)
        {
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
            //gameObject.GetComponent<PhotonTransformViewPositionControl>().SetSynchronizedValues(newSpeed, 0f);
            gameObject.GetComponent<PhotonTransformViewClassic>().SetSynchronizedValues(newSpeed, 0f);
            //gameObject.GetComponent<PhotonTransformViewScaleControl>().GetNetworkScale();

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
            if (Input.GetAxis("Horizontal") < 0)
            {
                newScale.x = -1;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                newScale.x = 1;
            }
            transform.localScale = newScale;
        }
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
