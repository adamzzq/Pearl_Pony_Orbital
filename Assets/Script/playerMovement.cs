using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
   
    public float runspeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }

    }
    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
}
