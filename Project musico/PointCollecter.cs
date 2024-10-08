using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PointCollecter : MonoBehaviour
{
    //public Activator activator;
    public TextMeshProUGUI scoreText;

    public string tagToCount = "Note"; // Change this to your assigned tag
    private int objectCount;

    private int currentScore = 0;
    private int maxScore;

    public Slider slider;

    public int redValue;
    public int orangeValue;
    public int greenValue;

    public int remainingNotes;

    public UnityEvent events;
    public bool hasCheckedScore;

    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;

    public GameObject[] notePatterns; // An array of GameObjects representing note patterns.
    private int currentPatternIndex = 0;

    public void AddScore(int _score)
    {
        currentScore += _score;
        scoreText.text = "Score: " + currentScore.ToString();

        slider.value = currentScore;

        remainingNotes--;
    }
    private void Start()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCount);
        objectCount = objectsWithTag.Length;

        // Print the count to the console
        Debug.Log("Number of " + tagToCount + " objects in the scene: " + objectCount * 100);

        maxScore = objectCount * 100;

        slider.maxValue = maxScore;

        remainingNotes = objectCount;

        //the slider can't be moved by the player
        slider.interactable = false;

        // max points / 3 so you have the red zone at 1/3 of the slider
        redValue = maxScore / 3;
        //Debug.Log(redValue);
        // max points / 3 so you have the orange zone at 2/3 of the slider
        orangeValue = maxScore / 3 * 2;
        //Debug.Log(orangeValue);
        // max points / 3 so you have the green zone at 3/3 of the slider
        greenValue = maxScore / 3 * 3;
        //Debug.Log(greenValue);

        

    }

    private void SpawnNotesForCurrentPattern()
    {
        if (currentPatternIndex < notePatterns.Length)
        {
            // Check if the enemy is alive before spawning notes.
            if (!enemyHealth.IsDead())
            {
                // Enable the notes for the current pattern.
                notePatterns[currentPatternIndex].SetActive(true);

                // Update the remaining notes count for the new pattern.
                int notesInCurrentPattern = notePatterns[currentPatternIndex].transform.childCount;
                remainingNotes = notesInCurrentPattern;

                // Adjust scoring thresholds based on the number of notes in the current pattern.
                // ...
                redValue = notesInCurrentPattern * 30;      // Adjust as needed
                orangeValue = notesInCurrentPattern * 60;   // Adjust as needed
                greenValue = notesInCurrentPattern * 90;    // Adjust as needed
                maxScore = notesInCurrentPattern * 100;     // Adjust as needed
                slider.maxValue = maxScore;
                // Reset hasCheckedScore to false for the new pattern.
                hasCheckedScore = false;
            }
            else
            {
                // Enemy is dead, you can take appropriate actions here.

                Debug.Log("ded");
            }
        }
        else
        {
            // All patterns are completed, you can end the game or take other actions.
            Debug.Log("um everything is GONE");
        }
    }



    public void ResetScore()
    {
        currentScore = 0;
        slider.value = 0;
        scoreText.text = "Score: " + currentScore.ToString();

        slider.value = currentScore;

    }


    void CheckScore()
    {
        
        if (currentScore <= redValue)
        {
            //events.Invoke();
            playerHealth.TakeDamage(1);
            Debug.Log("red");
        }
        else if (currentScore <= orangeValue)
        {
            Debug.Log("orange");
            enemyHealth.TakeDamage(1);
        }
        else if (currentScore <= greenValue)    
        {
            enemyHealth.TakeDamage(2);
            Debug.Log("green");
        }
    }



    public void UpdateNotes()
    {
        remainingNotes--;
        //Debug.Log(remainingNotes);
    }
    void Update()
    {
        if (remainingNotes <= 0 && !hasCheckedScore)
        {
            CheckScore();

            hasCheckedScore = true;

            // Pattern completed, move to the next pattern.
            currentPatternIndex++;
            Debug.Log("currentPattern " + currentPatternIndex);
            //ResetScore();
            Invoke("ResetScore", 2);
            Invoke("SpawnNotesForCurrentPattern", 2);


        }
        else if (playerHealth.IsDead() == true)
        {
            GameManager.instance.GameOver();
        }
    }
}
