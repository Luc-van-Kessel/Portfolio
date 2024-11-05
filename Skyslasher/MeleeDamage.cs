using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public float DamageAmount = 10f;

    [SerializeField] private float _DOTDamage;
    [SerializeField] private float _DOTInterval;

    private float _time;

    private bool _hasHit = false;
    public GameObject hitFXPrefab;

    private bool _isSlowMotionActive = false;

    [SerializeField] private LayerMask enemyLayers; 

    private void OnEnable()
    {
        SlowMotion.OnSlowMotionActivated += HandleSlowMotionActivated;
        SlowMotion.OnSlowMotionDeactivated += HandleSlowMotionDeactivated;
    }

    private void OnDisable()
    {
        SlowMotion.OnSlowMotionActivated -= HandleSlowMotionActivated;
        SlowMotion.OnSlowMotionDeactivated -= HandleSlowMotionDeactivated;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision, DamageAmount);
        _time = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_time >= _DOTInterval)
        {
            DealDamage(collision, _DOTDamage);
            _time = 0;
        }

        _time += Time.unscaledDeltaTime;
    }

    private void DealDamage(Collision collision, float damage)
    {
        //Check if the object is collided with specific layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            InstantiateHitFX(collision.contacts[0].point);
        }

        // check if the object is collided with layermask
        if (enemyLayers == (enemyLayers | (1 << collision.gameObject.layer)))
        {
            return;
        }

        if (collision.gameObject.GetComponent<ISlowMotionVulnerable>() != null)
        {
            if (!_isSlowMotionActive)
            {
                return;
            }
        }

        if (collision.gameObject.TryGetComponent(out Health entity) && _hasHit == false)
        {
            _hasHit = true;
            entity.TakeDamage(damage);
        }

        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage();
        }

        _hasHit = false;
    }

    private void HandleSlowMotionActivated()
    {
        _isSlowMotionActive = true;
    }

    private void HandleSlowMotionDeactivated()
    {
        _isSlowMotionActive = false;
    }

    private void InstantiateHitFX(Vector3 position)
    {
        if (hitFXPrefab != null)
        {
            GameObject hitFX = Instantiate(hitFXPrefab, position, Quaternion.identity);
            Destroy(hitFX, 2f);
        }
    }
}
