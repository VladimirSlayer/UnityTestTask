using UnityEngine;

public class SpeedBonus : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private string _logEntry = "Подобран бонус скорости";

    private PooledItem pooledItem;

    private void Awake()
    {
        pooledItem = GetComponent<PooledItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ApplySpeedBonus(_speedMultiplier, _duration);
            pooledItem.DeactivateAndRespawn();

            PickupLogManager.Instance.AddEntry(_logEntry);
        }
    }
}
