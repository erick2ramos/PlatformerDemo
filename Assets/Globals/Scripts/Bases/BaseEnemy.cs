using UnityEngine;

// Base class for enemies handling stats and collision resolve
public class BaseEnemy : MonoBehaviour
{
    public delegate void EnemyState();
    public float timeAttacking = 3.0f;

    protected Transform target;

    protected float timer = 0.0f;
    protected float minimalDistance = 5.0f;
    protected float speed;
    protected float collisionDamage;
    protected Vector3 attackDirection;

    public virtual void Init()
    {
        target = MainManager.Get.gameManager.player.transform;
        speed = MainManager.Get.settings.data.enemySpeed;
        collisionDamage = MainManager.Get.settings.data.enemyCollisionDamage;
    }

    // Checks for collision with player to do damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage((int)collisionDamage);
        }
    }
}
