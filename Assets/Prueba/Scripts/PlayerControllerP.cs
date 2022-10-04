using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerP : MonoBehaviour
{
     // A reference to the Rigidbody component 
    private Rigidbody rb;
    
    [Tooltip("How fast the ball moves left/right")]
    [Range(0, 10)]
    public float dodgeSpeed = 5;
    
    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    private int rail = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Get access to our Rigidbody component 
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// FixedUpdate is called at a fixed framerate and is a prime place to put
    /// Anything based on time.
    /// </summary>
    private void FixedUpdate()
    {
        // Check if we're moving to the side 
        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // Move Left
        if (Input.GetKeyDown(KeyCode.D) && rb.transform.position != new Vector3(2, rb.transform.position.y, rb.transform.position.z))
        {
            rb.transform.position += new Vector3(2, 0, 0);
            rail++;
        }
        // Move Right
        if (Input.GetKeyDown(KeyCode.A) && rb.transform.position != new Vector3(-2, rb.transform.position.y, rb.transform.position.z))
        {
            rb.transform.position += new Vector3(-2, 0, 0);
        }
        rb.AddForce(0, 0, rollSpeed);
    }
}