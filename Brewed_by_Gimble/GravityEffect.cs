using UnityEngine;
using System.Collections;
using Unity.FPS.Gameplay;

[CreateAssetMenu(fileName = "GravityEffect", menuName = "Potions/Effects/Gravity")]
public class GravityEffect : PotionEffect
{
    public float GravityMultiplier = 0.5f;
    public float PositionOffset = 0.5f; // Offset to prevent clipping

    public override void ApplyEffect(GameObject target)
    {
        PlayerCharacterController playerController = target.GetComponent<PlayerCharacterController>();
        if (playerController != null)
        {
            playerController.GravityDownForce *= GravityMultiplier;
            target.AddComponent<CoroutineHandler>().StartCoroutine(RotateOverTime(target, Quaternion.Euler(0, 0, 180f), 1f));
            Debug.Log($"Gravity effect applied with multiplier: {GravityMultiplier}");
        }
    }

    private IEnumerator RotateOverTime(GameObject target, Quaternion targetRotation, float duration)
    {
        PlayerCharacterController playerController = target.GetComponent<PlayerCharacterController>();
        playerController.GravityDownForce = -20;

        Vector3 initialPosition = target.transform.position;
        Quaternion initialRotation = target.transform.rotation;
        float elapsedTime = 0;

        // Move slightly up before rotating upside down
        target.transform.position += Vector3.up * PositionOffset;

        while (elapsedTime < duration)
        {
            target.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.transform.rotation = targetRotation;
        yield return new WaitForSeconds(10);
        playerController.GravityDownForce = 20;

        targetRotation = Quaternion.Euler(0, 0, 0);
        elapsedTime = 0;

        // Move slightly down before rotating back to normal
        target.transform.position -= Vector3.up * PositionOffset;

        while (elapsedTime < duration)
        {
            target.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.transform.rotation = targetRotation;
    }
}