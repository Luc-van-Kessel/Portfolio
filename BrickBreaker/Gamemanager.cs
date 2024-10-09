using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public GameObject ballPrefab; // Assign the ball prefab in the Inspector
    private BallManager currentBall;
    private GameObject paddle;
    private bool isGameStarted = false;
    public Transform spawnpoint;
    public int score = 0;
    public int lives = 3;
    private PaddleController controller;

    private void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        controller = paddle.GetComponent<PaddleController>();
    }

    private void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        // Instantiate the ball prefab
        GameObject ballObject = Instantiate(ballPrefab, spawnpoint.position, Quaternion.identity);
        currentBall = ballObject.GetComponent<BallManager>();
        isGameStarted = true;

        // Set the direction of the new ball to match the initial direction
        currentBall.SetDirection(currentBall.GetDirection());
        controller.canMove = true;
    }

    // You can also have methods for losing lives and other game management tasks
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            //restart scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            // Reset the ball
            isGameStarted = false;
            currentBall = null;
            controller.canMove = false;
            controller.ResetPaddle();
        }

    }

    public void AddScore(int points)
    {
        score += points;
    }
}
