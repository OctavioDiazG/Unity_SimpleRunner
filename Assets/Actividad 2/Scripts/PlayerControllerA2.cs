using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA2 : MonoBehaviour
{
    // A reference to the Rigidbody component 
    private Rigidbody rb;
    private bool grounded  = false;
    public float gravity = 1.81f;
    public float jumpHeight = 2.5f;
    
    /*[Tooltip("How fast the ball moves left/right")]
    [Range(0, 10)]
    public float dodgeSpeed = 5;*/
    
    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        // Get access to our Rigidbody component 
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.D) && rb.transform.position != new Vector3(2, rb.transform.position.y, rb.transform.position.z))
        {
            rb.transform.position += new Vector3(2, 0, 0);
            

        }
        // Move Right
        if (Input.GetKeyDown(KeyCode.A) && rb.transform.position != new Vector3(-2, rb.transform.position.y, rb.transform.position.z))
        {
            rb.transform.position += new Vector3(-2, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            grounded = false;
            rb.velocity = new Vector3(rb.velocity.x, CalculateJumpVerticalSpeed(), rb.velocity.z);
        }
    }
    
    void OnCollisionStay()
    {
        grounded = true;
        Debug.Log("en el piso");
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    /// <summary>
    /// FixedUpdate is called at a fixed framerate and is a prime place to put
    /// Anything based on time.
    /// </summary>
    private void FixedUpdate()
    {
        // Check if we're moving to the side 
        //var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
        rb.AddForce(0, 0, rollSpeed);
    }
}