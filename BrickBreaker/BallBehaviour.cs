using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float ballSpeed = 5f;
    private Vector2 ballDirection = Vector2.one.normalized;

    void Update()
    {
        // Move the ball
        transform.Translate(ballSpeed * ballDirection * Time.deltaTime);

        // Check for collision with screen boundaries
        if (transform.position.x < -16.2f || transform.position.x > 16.2f)
        {
            ballDirection.x = -ballDirection.x; // Reflect horizontally
        }

        if (transform.position.y > 8.5f)
        {
            ballDirection.y = -ballDirection.y; // Reflect vertically
        }   

        if (transform.position.y < -8.5f)
        {
            ballDirection.y = Mathf.Abs(ballDirection.y); // Reflect vertically (upward)
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        ballDirection = newDirection;
    }
}
