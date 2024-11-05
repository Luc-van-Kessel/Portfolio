using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public static ResultManager instance;
    public bool hasWon = false;
    public int score = 0;
    public GameObject losePanel;

    public delegate void PointLostEvent();
    public static event PointLostEvent OnPointLost;

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
        Debug.Log(score);
        score--; 
        OnPointLost?.Invoke(); 
    }

    private void Dead()
    {
        // if the player reaches the losing score, the player loses
        if (score == -5)
        {
            losePanel.SetActive(true); 
        }
    }

    private void WinningScore()
    {
        // if the player reaches the winning score, the player wins
        if (score >= 5)
        {
            hasWon = true;
        }
    }
}
