using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private float timeToLoadScene; 
    
    public void Kill()
    {
        gameObject.SetActive(false);
        Invoke("SceneLoader", 0.5f);
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
