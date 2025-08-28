using UnityEngine;

public class PooledItem : MonoBehaviour
{
    private ItemSpawner spawner;
    private ObjectPool pool;

    public void SetSpawner(ItemSpawner spawner, ObjectPool pool)
    {
        this.spawner = spawner;
        this.pool = pool;
    }

    public void DeactivateAndRespawn()
    {
        spawner.Respawn(gameObject, pool);
    }
}
