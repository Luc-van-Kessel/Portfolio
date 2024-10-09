using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float paddleSpeed = 5f;
    public float minX = -14.2f; 
    public float maxX = 14.2f; 
    public bool canMove = false;
    private Vector3 initialPosition; 
    
    private void Start()
    {
        initialPosition = transform.position;
    } 
    
    void Update()
    {
        if (canMove)
        {
            // Get the current mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; 
            float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, 0f);
        }
    }

    public void ResetPaddle()
    {
        transform.position = initialPosition;
    }
}
    
