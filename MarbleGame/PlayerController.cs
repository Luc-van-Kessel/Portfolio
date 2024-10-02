using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    private float xInput;
    private float zInput;
    public float moveSpeed;

    float maxSpeed = 10.0f; // replace 10.0f with your desired maximum speed

    public float jumpForce;
    [SerializeField]private bool isGrounded;
    
    private bool jump;
    private int groundContacts = 0;
    //private Vector3 originalScale;

    private Death dead;

    void Start()
    {
        dead = GetComponent<Death>();
        //originalScale = transform.localScale;
        //// Set the scale of the player to a fixed value
        //transform.localScale = new Vector3(2, 2, 2);
    }
    void Update()
    {
        MovePlayerRelativeToCamera();
        //if the camera moves the ball rotates with its own transform too with transform.forward and transform.right local
        //Good for handling inputs or animation
        ProcessInputs();


    }

    private void FixedUpdate()
    {
        MovePlayerRelativeToCamera();
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        Vector3 forwardRelativeVerticalInput = zInput * Vector3.ProjectOnPlane(forward, Vector3.up);
        Vector3 rightRelativeVerticalInput = xInput * right;
 
        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;

        //Debug.Log(cameraRelativeMovement);
        if (cameraRelativeMovement.magnitude > maxSpeed)
        {
            cameraRelativeMovement = cameraRelativeMovement.normalized * maxSpeed;
        }

        rb.AddForce(cameraRelativeMovement * moveSpeed);


        if (jump == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
            isGrounded = false;

        }
    }      
    void ProcessInputs()
    {
        zInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
        else if (!isGrounded)
        {
            jump = false;
        }
    }
    void MovePlayerRelativeToCamera()
    {
       
     

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("grounded");
            groundContacts++;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            groundContacts--;
            if (groundContacts == 0)
            {
                Debug.Log("not grounded");
                isGrounded = false;
            }
        }
    }
}
