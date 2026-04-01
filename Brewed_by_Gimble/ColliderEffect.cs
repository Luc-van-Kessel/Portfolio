using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ColliderEffect", menuName = "Potions/Effects/ColliderEffect")]
public class ColliderEffect : PotionEffect
{
    public bool enableCollider = true;
    public bool enableAllColliders = true;
    public bool DisableIntimer;

    public override void ApplyEffect(GameObject target)
    {
        Collider collider = target.GetComponent<Collider>();
        if (collider != null)
        {
            Debug.Log("doing something..");
            collider.enabled = enableCollider;
        }

        if (DisableIntimer)
        {
            target.AddComponent<CoroutineHandler>().StartCoroutine(ReDisableCollider(target));

        }

        if (!enableAllColliders)
            return;
        Collider[] colliders = target.GetComponents<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = enableCollider;
        }
    }

    private IEnumerator ReDisableCollider(GameObject target)
    {
        yield return new WaitForSeconds(20);

        Collider collider = target.GetComponent<Collider>();
        if (collider != null)
        {
            Debug.Log("doing something..");
            collider.enabled = false;
        }

    }
}