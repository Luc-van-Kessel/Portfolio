using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    private BallManager ballManager;
    private GameObject topPaddle;
    private GameObject BottomPaddle;

    void Start()
    {
        BottomPaddle = GameObject.FindGameObjectWithTag("BottomPaddle");
        topPaddle = GameObject.FindGameObjectWithTag("TopPaddle");
        ballManager = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallManager>();
    }

    void Update()
    {
        BottomBounceBehaviour1();
        TopBounceBehaviour();
    } 
 
    private void BottomBounceBehaviour1()
    {
        // Check for collision with paddle using AABB collision detection
        if (BottomPaddle != null)
        {
            Bounds paddleBounds = new Bounds(BottomPaddle.transform.position, BottomPaddle.GetComponent<SpriteRenderer>().bounds.size);
            Bounds ballBounds = GetComponent<SpriteRenderer>().bounds;

            if (ballBounds.Intersects(paddleBounds))
            {
                // Calculate reflection direction based on contact point
                Vector3 contactPoint = new Vector3(
                    Mathf.Clamp(transform.position.x, paddleBounds.min.x, paddleBounds.max.x),
                    BottomPaddle.transform.position.y,
                    0f
                );

                float relativeIntersectX = contactPoint.x - BottomPaddle.transform.position.x;
                float normalizedRelativeIntersectX = relativeIntersectX / (paddleBounds.size.x * 0.5f);

                float bounceAngle = normalizedRelativeIntersectX * 60f; // Adjust bounce angle as needed

                // Calculate new bounce direction
                Vector2 newDirection = new Vector2(Mathf.Sin(bounceAngle * Mathf.Deg2Rad), -Mathf.Abs(Mathf.Cos(bounceAngle * Mathf.Deg2Rad))).normalized;
                ballManager.SetDirection(newDirection);
            }
        }
    } 

    private void TopBounceBehaviour()
    {
        if (topPaddle != null)
        {
            Bounds paddleBounds = new Bounds(topPaddle.transform.position, topPaddle.GetComponent<SpriteRenderer>().bounds.size);
            Bounds ballBounds = GetComponent<SpriteRenderer>().bounds;

            if (ballBounds.Intersects(paddleBounds))
            {
                // Calculate reflection direction based on contact point
                Vector3 contactPoint = new Vector3(
                    Mathf.Clamp(transform.position.x, paddleBounds.min.x, paddleBounds.max.x),
                    topPaddle.transform.position.y,
                    0f
                );

                float relativeIntersectX = contactPoint.x - topPaddle.transform.position.x;
                float normalizedRelativeIntersectX = relativeIntersectX / (paddleBounds.size.x * 0.5f);

                float bounceAngle = normalizedRelativeIntersectX * 60f; // Adjust bounce angle as needed

                // Calculate new bounce direction
                Vector2 newDirection = new Vector2(Mathf.Sin(bounceAngle * Mathf.Deg2Rad), Mathf.Cos(bounceAngle * Mathf.Deg2Rad)).normalized;
                ballManager.SetDirection(newDirection);
            }
        }
    }
}

