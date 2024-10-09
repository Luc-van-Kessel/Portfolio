using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public static ResultManager instance;

    // Check if the player has won or lost
    public bool hasWon = false;

    // check the player's score
    public int score = 0;

    public GameObject LosePanel;

    public delegate void PointLostEvent();
    public static event PointLostEvent OnPointLost;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        Dead();
        WinningScore();
    }

    public void LosePoint()
    {
        Debug.Log("OUCH");
        Debug.Log(score);
        score--; 
        OnPointLost?.Invoke(); 
    }

    private void Dead()
    {
        if (score == -5)
        {
            LosePanel.SetActive(true); 
            Debug.Log("You lost");
        }
    }

    private void WinningScore()
    {
        // if the player reaches the winning score, the player wins
        if (score >= 5)
        {
            Debug.Log("You won");
            hasWon = true;
        }
    }
}
