using UnityEngine;

public class PickUp : MonoBehaviour
{
    //if in trigger collider pick up object
    private void OnTriggerEnter(Collider other)
    {
        PickUpCollecter pickupCollector = other.GetComponent<PickUpCollecter>();

        if (pickupCollector != null)
        {
            pickupCollector.PickupCollected();
            gameObject.SetActive(false);
        }
    }
}
