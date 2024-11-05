using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.transform.name == "Player")
        {
            // Set the parent of the player to the sticky platform
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.transform.name == "Player")
        {
            // Remove the parent of the player
            other.transform.SetParent(null);
        }
    }
}
