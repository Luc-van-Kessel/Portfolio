using UnityEngine;
using TMPro;
public class PickUpUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
   
    public void UpdateText(PickUpCollecter pickupcollector)
    {
        text.text = pickupcollector.NumberOfPickUps.ToString();
    }
}
