using UnityEngine;

public class DamageCube : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 100f;
    [SerializeField] private string _logEntry = "Подобран куб урона";
    private PooledItem pooledItem;

    private void Awake()
    {
        pooledItem = GetComponent<PooledItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthController health = other.GetComponent<HealthController>();
        if (health != null)
        {
            health.ApplyDamage(_damageAmount);
            UIManager.Instance.RefreshHealth(health.Health);
            pooledItem.DeactivateAndRespawn();
            PickupLogManager.Instance.AddEntry(_logEntry);
        }
    }
}
