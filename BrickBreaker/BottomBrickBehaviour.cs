using UnityEngine;

public class BottomBrickBehaviour : MonoBehaviour
{
    private BallManager ballManager;

    void Start()
    {
        ballManager = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallManager>();
    }

    void Update()
    {
        // Check for collision with ball using AABB collision detection
        Bounds brickBounds = new Bounds(transform.position, GetComponent<SpriteRenderer>().bounds.size);
        Bounds ballBounds = ballManager.GetComponent<SpriteRenderer>().bounds;

        if (ballBounds.Intersects(brickBounds))
        {
            // Calculate reflection direction based on contact point
            Vector3 contactPoint = new Vector3(
                Mathf.Clamp(ballManager.transform.position.x, brickBounds.min.x, brickBounds.max.x),
                transform.position.y,
                0f
            );

            float relativeIntersectX = contactPoint.x - transform.position.x;
            float normalizedRelativeIntersectX = relativeIntersectX / (brickBounds.size.x * 0.5f);

            float bounceAngle = normalizedRelativeIntersectX * 60f; // Adjust bounce angle as needed

            // Calculate new bounce direction
            Vector2 newDirection = new Vector2(Mathf.Sin(bounceAngle * Mathf.Deg2Rad), -Mathf.Abs(Mathf.Cos(bounceAngle * Mathf.Deg2Rad))).normalized;
            ballManager.SetDirection(newDirection);
        }

        // Other brick behavior
        // ...
    }
}
