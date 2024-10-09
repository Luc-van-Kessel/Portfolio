using UnityEngine;

public class CameraMouseController : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    public bool rotateAroundPlayer = true;
    public float rotationSpeed = 5.0f; 
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    private void Update()
    {
       
        if (Input.GetMouseButton(1))
        {
            rotateAroundPlayer = true;
        }
        else
        {
            rotateAroundPlayer = false;
        }
    }

    private void LateUpdate()
    {
        if (rotateAroundPlayer)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Calculate the horizontal and vertical rotation angles
            Quaternion camTurnAngleX = Quaternion.AngleAxis(mouseX, Vector3.up);
            Quaternion camTurnAngleY = Quaternion.AngleAxis(-mouseY, Vector3.right);

            // Apply the rotation to the camera offset vector
            _cameraOffset = camTurnAngleY * camTurnAngleX * _cameraOffset;
        }
        //make a new position for the camera
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        //smoothly move the camera to the new position
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        //make sure the camera is looking at the player
        transform.LookAt(PlayerTransform);
    }
}
