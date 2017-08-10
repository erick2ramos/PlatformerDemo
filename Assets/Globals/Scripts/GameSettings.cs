using UnityEngine;

[CreateAssetMenu(menuName = "Game Setting")]
public class GameSettings : ScriptableObject {
    [Header("Player stats")]
    public float playerSpeed;
    public float playerJumpForce;
    public int playerMaxLife;

    [Header("Enemy stats")]
    public float enemySpeed;
    public int enemyCollisionDamage;
    public float enemySpawnChance;

    [Header("Projectile stats")]
    public float bulletSpeed;
    public int bulletDamage;

    [Header("Collectables stats")]
    public int collectableScoreValue;

    public static float defaultPlayerSpeed = 5;
    public static float defaultPlayerJumpForce = 8;
    public static int defaultPlayerMaxLife = 5;

    public static float defaultEnemySpeed = 5;
    public static int defaultEnemyCollisionDamage = 1;
    public static float defaultEnemySpawnChance = 0.5f;

    public static float defaultBulletSpeed = 5;
    public static int defaultBulletDamage = 1;

    public static int defaultCollectableScoreValue = 1;
}
