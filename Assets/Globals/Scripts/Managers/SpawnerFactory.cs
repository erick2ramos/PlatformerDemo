using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    WalkE,
    ShootE,
    Count
}

public class SpawnerFactory : MonoBehaviour {
    public GameObject[] enemyPrefabs;
    public GameObject collectablePrefab;

    List<BaseEnemy> spawnedEnemies = new List<BaseEnemy>();
    List<CollectableController> spawnedCollectables = new List<CollectableController>();

    public void Init()
    {
        Clear();
    }

    public GameObject CreateEnemy(EnemyType type, Vector3 at)
    {
        GameObject go = Instantiate(enemyPrefabs[(int)type], at, Quaternion.identity);
        go.GetComponent<BaseEnemy>().ChangeState(BaseEnemy.AIState.Inactive);
        spawnedEnemies.Add(go.GetComponent<BaseEnemy>());
        return go;
    }

    public GameObject CreateCollectable(Vector3 at)
    {
        GameObject go = Instantiate(collectablePrefab, at, Quaternion.identity);
        spawnedCollectables.Add(go.GetComponent<CollectableController>());
        return go;
    }

    public void ActivateSpawnedEnemies()
    {
        foreach (BaseEnemy enemy in spawnedEnemies)
        {
            enemy.Init();
            enemy.ChangeState(BaseEnemy.AIState.Idle);
        }
    }

    public void DeactivateSpawnedEnemies()
    {
        foreach (BaseEnemy enemy in spawnedEnemies)
        {
            enemy.ChangeState(BaseEnemy.AIState.Inactive);
        }
    }

    // Maybe is more beneficial to reuse instantiated GOs
    public void Clear()
    {
        while(spawnedCollectables.Count > 0)
        {
            CollectableController ptr = spawnedCollectables[0];
            spawnedCollectables.RemoveAt(0);
            if(ptr != null)
            {
                Destroy(ptr.gameObject);
            }
        }

        while (spawnedEnemies.Count > 0)
        {
            BaseEnemy ptr = spawnedEnemies[0];
            spawnedEnemies.RemoveAt(0);
            Destroy(ptr.gameObject);
        }
        spawnedEnemies.Clear();
        spawnedCollectables.Clear();
    }
}
