using UnityEngine;

public class Brick : MonoBehaviour
{
    Gamemanager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<Gamemanager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallManager ballManager = collision.gameObject.GetComponent<BallManager>();

            if (ballManager != null)
            {
                // Calculate reflection direction based on contact normal
                Vector2 reflectionDirection = Vector2.Reflect(ballManager.GetDirection(), collision.contacts[0].normal);
                ballManager.SetDirection(reflectionDirection);
            }

            // Destroy the brick upon collision
            Destroy(gameObject, 0.001f);

            // Increment the score

            gamemanager.AddScore(1);
        }
    }
}
