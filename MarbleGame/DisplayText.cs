using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public string message;
    public TextMeshProUGUI text;
    public GameObject panel; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = message;
            text.gameObject.SetActive(true);
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            panel.SetActive(false);
        }
    }
}
