using UnityEngine;
using TMPro;
public class PickUpUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
   
    public void UpdateText(PickUpCollecter pickupcollector)
    {
        text.text = pickupcollector.NumberOfPickUps.ToString();
    }
}
