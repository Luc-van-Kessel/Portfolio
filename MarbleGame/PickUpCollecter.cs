using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PickUpCollecter : MonoBehaviour
{
    // Public integer property that represents the number of pickups collected.
    // This property can be read by any code that has access to an instance of the class,
    // but can only be modified from within the class itself.
    public int NumberOfPickUps { get; private set; }
    public UnityEvent<PickUpCollecter> OnPickupCollected; 
    
    public void PickupCollected()
    {        
        NumberOfPickUps++;
        // Invoke the OnPickupCollected event, passing the current instance of the PickUpCollecter class as a parameter
        OnPickupCollected.Invoke(this);
    }
}
