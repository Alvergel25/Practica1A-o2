using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, runningSpeed, jumpforce, mouseSens, sphereRadius;
    public string groundName;

    private Rigidbody rb;
    private float x, z, mouseX;
    private bool jumpPressed, shiftPressed;
    private float currentSpeed;
    private Vector3 movementVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        shiftPressed = Input.GetKey(KeyCode.LeftShift);
        mouseX = Input.GetAxisRaw("Mouse X");
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpPressed = true;
        }

        RotatePlayer();

    }

    void RotatePlayer()
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * mouseSens * Time.deltaTime;
        transform.Rotate(rotation);
    }

    private void FixedUpdate()
    {
        ApplySpeed();

        ApplyJumpForce();
    }

    void ApplySpeed()
    {
        if(shiftPressed)
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
        movementVector = (transform.forward * currentSpeed * z) + (transform.right * currentSpeed * x) + new Vector3(0, rb.velocity.y, 0);
        rb.velocity = movementVector;
    }

    public Vector3 GetMovementVector()
    {
        return movementVector;
    }

    void ApplyJumpForce()
    {
        if (jumpPressed)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpforce);
            jumpPressed = false;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit[] colliders = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius, Vector3.up);

        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].collider.gameObject.layer == LayerMask.NameToLayer(groundName))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }
}
