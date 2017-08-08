using UnityEngine;

// Base class for enemies handling stats and collision resolve
public class BaseEnemy : MonoBehaviour
{
    // State machine fields
    public delegate void EnemyState();
    protected EnemyState[] states;
    public enum AIState
    {
        Inactive,
        Idle,
        Attack,
        Count
    };
    public AIState currentState;
    public AIState nextState;

    public float timeAttacking = 3.0f;
    public float minimalDistance = 5.0f;

    protected Transform target;

    protected float timer = 0.0f;
    protected float speed;
    protected float collisionDamage;
    protected Vector3 attackDirection;

    // Sets default values from settigs asset file
    public virtual void Init()
    {
        target = MainManager.Get.gameManager.player.transform;
        speed = MainManager.Get.settings.data.enemySpeed;
        collisionDamage = MainManager.Get.settings.data.enemyCollisionDamage;
    }

    void Update()
    {
        if (nextState != currentState)
        {
            currentState = nextState;
        }
        else
        {
            states[(int)currentState]();
        }
    }

    // Checks for collision with player to do damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage((int)collisionDamage);
        }
    }

    public void ChangeState(AIState next)
    {
        nextState = next;
    }
}
