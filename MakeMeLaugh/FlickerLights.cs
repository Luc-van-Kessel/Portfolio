using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float timeDelay2;
    public float timeDelay3;

    void Start()
    {
        // Subscribe to the point loss event
        ResultManager.OnPointLost += ReactToPointLoss;
    }

    private void Update()
    {
        if (isFlickering)
        {
            StartCoroutine(Flickering2());
        }
    } 
    
    void ReactToPointLoss()
    {
        isFlickering = false;
        if (!isFlickering)
        {
            StartCoroutine(Flicker());
        }
    } 
    
    private IEnumerator Flicker()
    {
        isFlickering = true;
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay2);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay3);
        GetComponent<Light>().enabled = true;

    } 

    private IEnumerator Flickering2()
    {
        isFlickering = false;
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = false;
        yield return new WaitForSeconds(timeDelay);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay2);
        GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(timeDelay3);
    }
}
