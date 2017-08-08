using UnityEngine;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour
{
    public Transform joinPoint;
    public Transform[] spawnPoints;

    public void SpawnObjectsOnPois()
    {
        if (spawnPoints.Length == 0) return;
        // Obligatory collectable
        int selectedIndex = Random.Range(0, spawnPoints.Length);
        MainManager.Get.spawner.CreateCollectable(spawnPoints[selectedIndex].position);

        float chance = MainManager.Get.settings.data.enemySpawnChance;
        // Enemies with chance to spawn
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (selectedIndex != i)
            {
                if(Random.value < chance)
                {
                    EnemyType type = (Random.value < 0.5f) ? EnemyType.ShootE : EnemyType.WalkE;
                    MainManager.Get.spawner.CreateEnemy(type, spawnPoints[i].position);
                }
            }
        }
    }
}
