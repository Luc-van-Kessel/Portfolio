using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    private void Awake()
    {
        // Get the MeshRenderer component of this object.
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Store the original material.
            originalMaterial = spriteRenderer.material;
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on this object.");
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
        // Check if we have a valid MeshRenderer and flashMaterial.
        if (spriteRenderer != null && flashMaterial != null)
        {
            // Swap to the flashMaterial.
            spriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(duration);

            // Restore the original material.
            spriteRenderer.material = originalMaterial;
        }
        //else
        //{
        //    Debug.LogError("Invalid MeshRenderer or flashMaterial.");
        //}

        flashRoutine = null;
    }
}