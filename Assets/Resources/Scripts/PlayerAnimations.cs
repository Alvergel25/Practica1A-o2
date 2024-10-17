using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");
    //    bool shifPressed = Input.GetKey(KeyCode.LeftShift);

    //    if (z != 0 || z != 0)
    //    {
    //        //Si se esta moviendo
    //        if (shifPressed)//Si shift esta presionado
    //        {
    //            //Y ademas esta corriendo
    //            animator.SetBool("isRunning", true);
    //            animator.SetBool("isWalking", false);
    //        }
    //        else
    //        {
    //            //No esta corriendo, pero esta andando
    //            animator.SetBool("isRunning", false);
    //            animator.SetBool("isWalking", true);
    //        }
    //    }
    //    else
    //    {
    //        //Esta quieto
    //        animator.SetBool("isRunning", false);
    //        animator.SetBool("isWalking", false);
    //    }

    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", playerMovement.GetMovementVector().magnitude / playerMovement.runningSpeed);
    }
}
