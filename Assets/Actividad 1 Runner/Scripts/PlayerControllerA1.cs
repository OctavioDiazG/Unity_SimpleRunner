using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControllerA1 : MonoBehaviour
{
    public float gravity = 9.81f;
    public float jumpHeight = 2.5f;
    public float speed = 3.0f;
    private bool isDead = false;

    Rigidbody _rb3D;
    bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb3D = GetComponent<Rigidbody>();
        _rb3D.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        _rb3D.freezeRotation = true;
        _rb3D.useGravity = false;
        //defaultScale = transform.localScale;
    }

    /*void Update()
    {
        //Move Forward
        if (!isDead)
        {
            _rb3D.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
        // Move Left
        if (!isDead && Input.GetKeyDown(KeyCode.A) && _rb3D.transform.position != new Vector3(-3, _rb3D.transform.position.y,_rb3D.transform.position.z))
        {
            _rb3D.transform.position += new Vector3(-3, 0, 0);
        }
        // Move Right
        if (!isDead && Input.GetKeyDown(KeyCode.D) && _rb3D.transform.position != new Vector3(3, _rb3D.transform.position.y,_rb3D.transform.position.z))
        {
            _rb3D.transform.position += new Vector3(3, 0, 0);
        }
        // Jump
        if (isDead != true && Input.GetKeyDown(KeyCode.W) && grounded)
        {
            _rb3D.velocity = new Vector3(_rb3D.velocity.x, CalculateJumpVerticalSpeed(), _rb3D.velocity.z);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        // We apply gravity manually for more tuning control
        _rb3D.AddForce(new Vector3(0, -gravity * _rb3D.mass, 0));

        
        //Move Forward
        if (!isDead)
        {
            _rb3D.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
        // Move Left
        if (!isDead && Input.GetKeyDown(KeyCode.A) && _rb3D.transform.position != new Vector3(-3, _rb3D.transform.position.y,_rb3D.transform.position.z))
        {
            _rb3D.transform.position += new Vector3(-3, 0, 0);
        }
        // Move Right
        if (!isDead && Input.GetKeyDown(KeyCode.D) && _rb3D.transform.position != new Vector3(3, _rb3D.transform.position.y,_rb3D.transform.position.z))
        {
            _rb3D.transform.position += new Vector3(3, 0, 0);
        }
        // Jump
        if (!isDead && Input.GetKeyDown(KeyCode.W) && grounded)
        {
            grounded = false;
            _rb3D.velocity = new Vector3(_rb3D.velocity.x, CalculateJumpVerticalSpeed(), _rb3D.velocity.z);
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            print("GameOver!");
            //HazardGenerator.instance.gameOver = true;
            isDead = true;
            if (isDead)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Game Over");
            }
        }
    }
}