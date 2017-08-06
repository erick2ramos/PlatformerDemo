using UnityEngine;

public enum EnemyType
{
    WalkE,
    ShootE
}

public class EnemyFactory : MonoBehaviour {
    public GameObject[] enemyPrefabs;
        
    public GameObject CreateEnemy(EnemyType type)
    {
        return Instantiate(enemyPrefabs[(int)type]);
    }
}
