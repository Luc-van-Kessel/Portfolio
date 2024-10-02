using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public string message;
    public TextMeshProUGUI text;
    public GameObject panel;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
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