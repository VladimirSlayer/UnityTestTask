using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool[] _itemPools;
    [SerializeField] private Vector2 spawnArea = new Vector2(2.5f, 2.5f);
    [SerializeField] private int _initialSpawnCount = 9;

    private void Start()
    {
        for (int i = 0; i < _initialSpawnCount; i++)
        {
            SpawnRandomItem();
        }
    }

    public void SpawnRandomItem()
    {
        List<ObjectPool> availablePools = new List<ObjectPool>();
        foreach (var pool in _itemPools)
        {
            if (pool.HasAvailableObjects) availablePools.Add(pool);
        }

        if (availablePools.Count == 0) return;

        ObjectPool selectedPool = availablePools[Random.Range(0, availablePools.Count)];
        GameObject item = selectedPool.GetFromPool();
        item.transform.position = GetRandomPosition();

        PooledItem pooledItem = item.GetComponent<PooledItem>();
        if (pooledItem != null)
        {
            pooledItem.SetSpawner(this, selectedPool);
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-spawnArea.x/2, spawnArea.x/2),
            0.5f,
            Random.Range(-spawnArea.y/2, spawnArea.y/2)
        );
    }

    public IEnumerator RespawnCoroutine(GameObject item, ObjectPool pool, float delay) {
        pool.ReturnToPool(item);
        yield return new WaitForSeconds(delay);
        SpawnRandomItem();
    }

    public void Respawn(GameObject item, ObjectPool pool, float delay = 3f) {
        StartCoroutine(RespawnCoroutine(item, pool, delay));
    }
}
