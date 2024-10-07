using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public EnemyStats stats;
    public float speed = 10f;
    public int maxBounces = 3;
    public float damage = 10;
    private Vector3 direction;
    private bool setTarget;

    private bool hitByPlayer = false;

    public GameObject HitFXPrefab;
    public AudioSource _ricochetSound;

    public LayerMask RicochetLayers;

    private Coroutine destroyBulletCoroutine;

    [HideInInspector] public Transform swordTransform;

    private void Awake()
    {
        speed = stats.fireVelocity;
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
        damage = stats.damage; 
    }

    private void OnEnable()
    {
        destroyBulletCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private void InstantiateHitFX(Vector3 position)
    {
        if (HitFXPrefab != null)
        {
            GameObject hitFX = Instantiate(HitFXPrefab, position, Quaternion.identity);
            Destroy(hitFX, 2f);
        }
    }

    void Update()
    {
        if (!setTarget)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        if (newTarget != null)
        {
            setTarget = true;
            direction = newTarget - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MeleeDamage>())
        {
            hitByPlayer = true;
        }

        int layer = collision.gameObject.layer;

        if ((RicochetLayers & (1 << layer)) != 0)
        {
            _ricochetSound.PlayOneShot(_ricochetSound.clip);
            if (maxBounces <= 0)
            {
                ObjectPoolManager.ReturnObjectToPool(gameObject);
                return;
            }

            InstantiateHitFX(collision.contacts[0].point);

            // Only remove count down the bounce when not touching a melee. This makis it the melee can infinitly hit the bullets
            if (collision.gameObject.GetComponent<MeleeDamage>() == null)
                maxBounces--;

            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflection = Vector3.Reflect(direction.normalized, normal.normalized);
            direction = AdjustRicochetDirection(reflection, collision);
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += direction * 0.1f;
            _ricochetSound.time = 0;

        }
        // everything else that is not a ricochet layer returns the object to the pool

        else if (collision.gameObject.TryGetComponent(out Health entity) && hitByPlayer)
        {
            entity.TakeDamage(damage);

            ObjectPoolManager.ReturnObjectToPool(gameObject);

            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage();
            }

            return;

        }
        else if (collision.gameObject.TryGetComponent(out Health entity2) && collision.gameObject.GetComponent<PlayerMarker>())
        {
            entity2.TakeDamage(damage);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage();
            }

            return;
        } 
        else
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        //ObjectPoolManager.ReturnObjectToPool(gameObject);

    }

    private Vector3 AdjustRicochetDirection(Vector3 reflection, Collision collision)
    {
        Vector3 adjustedDirection = -reflection;
        adjustedDirection = Vector3.Reflect(adjustedDirection, collision.contacts[0].normal);
        return adjustedDirection;
    }

    IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
