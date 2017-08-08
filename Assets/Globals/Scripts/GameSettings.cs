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

}
