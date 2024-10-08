using UnityEngine;

public class Brick : MonoBehaviour
{
    private Gamemanager gamemanager;
    private BallManager ballManager;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        CheckBallCollision();
    }

    private void CheckBallCollision()
    {
        // Check if the ball exists and is tagged as "Ball"
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballManager = ball.GetComponent<BallManager>();
            if (ballManager != null)
            {
                // AABB collision detection
                Bounds brickBounds = GetComponent<SpriteRenderer>().bounds;
                Bounds ballBounds = ball.GetComponent<SpriteRenderer>().bounds;

                if (ballBounds.Intersects(brickBounds))
                {
                    // Calculate reflection direction based on contact point
                    Vector3 contactPoint = ball.transform.position;
                    Vector3 closestPoint = new Vector3(
                        Mathf.Clamp(contactPoint.x, brickBounds.min.x, brickBounds.max.x),
                        Mathf.Clamp(contactPoint.y, brickBounds.min.y, brickBounds.max.y),
                        0f
                    );

                    // Calculate the reflection direction based on the contact normal
                    Vector2 reflectionDirection = Vector2.Reflect(ballManager.GetDirection(), (contactPoint - closestPoint).normalized);
                    ballManager.SetDirection(reflectionDirection);

                    // Destroy the brick upon collision
                    Destroy(gameObject, 0.001f);

                    // Increment the score
                    gamemanager.AddScore(1);
                }
            }
        }
    }
}
