using UnityEngine;

public class WalkEController : BaseEnemy {
    public LayerMask wallLayer;

    public override void Init()
    {
        base.Init();
    }
    
	void Start () {
        // Initializing state machine
        states = new EnemyState[(int)AIState.Count];
        states[(int)AIState.Inactive] = UpdInactive;
        states[(int)AIState.Idle] = UpdIdle;
        states[(int)AIState.Attack] = UpdAttack;
        
        Init();
	}
	
    // Delegated methods for each possible AI state in the state machine
    void UpdInactive()
    {

    }

    void UpdIdle()
    {
        if(Mathf.Abs(target.position.z - transform.position.z) < minimalDistance)
        {
            attackDirection = target.position.z < transform.position.z ? Vector3.back : Vector3.forward;
            transform.rotation = Quaternion.LookRotation(attackDirection);
            timer = timeAttacking;
            nextState = AIState.Attack;
        }
    }

    void UpdAttack()
    {
        if(timer <= 0.0f)
        {
            nextState = AIState.Idle;
        } else
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position + (Vector3.up * 0.5f + transform.forward ), transform.forward, out hit, 0.4f, wallLayer))
            {
                nextState = AIState.Idle;
            }
            // Simple gravity
            if(!Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 0.2f))
            {
                attackDirection.y = Vector3.down.y;
            } else
            {
                attackDirection.y = 0;
            }
            transform.Translate(attackDirection * speed * Time.deltaTime, Space.World);
            timer -= Time.deltaTime;
        }
    }
}
