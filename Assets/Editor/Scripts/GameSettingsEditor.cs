using UnityEngine;
using UnityEditor;

public class GameSettingsEditor : ScriptableObject {
    // Editor context method to set current selected Game Setting SO asset to default values
    [MenuItem("CONTEXT/GameSettings/Revert to default", priority = 0)]
    static void RevertToDefault()
    {
        GameSettings gs = (GameSettings)Selection.activeObject;

        gs.playerSpeed = GameSettings.defaultPlayerSpeed;
        gs.playerJumpForce = GameSettings.defaultPlayerJumpForce;
        gs.playerMaxLife = GameSettings.defaultPlayerMaxLife;

        gs.enemySpeed = GameSettings.defaultEnemySpeed;
        gs.enemyCollisionDamage = GameSettings.defaultEnemyCollisionDamage;
        gs.enemySpawnChance = GameSettings.defaultEnemySpawnChance;

        gs.bulletSpeed = GameSettings.defaultBulletSpeed;
        gs.bulletDamage = GameSettings.defaultBulletDamage;

        Debug.Log(gs.name + " reverted to default values");
    }
}
