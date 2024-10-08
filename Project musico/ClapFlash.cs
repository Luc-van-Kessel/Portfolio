using System.Collections;
using UnityEngine;

public class ClapFlash : MonoBehaviour
{
    [Tooltip("Sprite to switch to during the flash.")]
    [SerializeField] private Sprite flashSprite;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private Coroutine flashRoutine;

    private void Awake()
    {
        // Get the SpriteRenderer component of this object.
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Store the original sprite.
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on this object.");
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Check if we have a valid SpriteRenderer and flashSprite.
        if (spriteRenderer != null && flashSprite != null)
        {
            // Swap to the flashSprite.
            spriteRenderer.sprite = flashSprite;

            yield return new WaitForSeconds(duration);

            // Restore the original sprite.
            spriteRenderer.sprite = originalSprite;
        }
        else
        {
            Debug.LogError("Invalid SpriteRenderer or flashSprite.");
        }

        flashRoutine = null;
    }
}
