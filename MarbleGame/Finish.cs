using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int amountOfScenes = 1;
    public bool mouseLock;
    void Start()
    {
        if (mouseLock == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CompleteLevel();
        }
    }
    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + amountOfScenes);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - amountOfScenes);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
