using UnityEngine;

public enum EnemyType
{
    WalkE,
    ShootE
}

public class EnemyFactory : MonoBehaviour {
    public static Entity CreateEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.WalkE:
                {
                    break;
                }
            case EnemyType.ShootE:
                {
                    break;
                }
        }
        GameObject go = new GameObject();
        Entity e = go.AddComponent<Entity>();
        return e;
    }
}
