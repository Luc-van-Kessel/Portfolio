using UnityEngine;

public class Potion : MonoBehaviour
{
    public PotionData Data;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Make sure it's tagged as 'Player'.");
        }
    }
    public void ApplyEffects(GameObject target)
    {
        foreach (var effect in Data.Effects)
        {
            effect.ApplyEffect(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        var interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(Data); // Let the interactable handle the potion
            Destroy(gameObject);
        }
    }

    public void TryConsume(GameObject heldPotion)
    {
        // Check if this potion is the currently held potion
        if (gameObject == heldPotion)
        {
            ApplyEffects(player);
            Debug.Log($"{gameObject.name} consumed.");
            Destroy(gameObject); // Destroy the potion object after consuming
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} is not the held potion.");
        }
    }
}