using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string _noteText;
    [SerializeField] private string _logEntry = "Подобрана записка";

    private PooledItem pooledItem;

    private void Awake()
    {
        pooledItem = GetComponent<PooledItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowNote(_noteText);
            pooledItem.DeactivateAndRespawn();

            PickupLogManager.Instance.AddEntry(_logEntry);
        }
    }
}
