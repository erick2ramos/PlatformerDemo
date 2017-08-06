using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEController : BaseEnemy {
    // WalkE state machine
    EnemyState[] states;
    public enum WalkEState
    {
        Idle,
        Attack,
        Count
    };
    public WalkEState currentState;
    public WalkEState nextState;
    

    public override void Init()
    {
        base.Init();
        nextState = WalkEState.Idle;
    }
    
	void Start () {
        // Initializing state machine
        states = new EnemyState[(int)WalkEState.Count];
        states[(int)WalkEState.Idle] = UpdIdle;
        states[(int)WalkEState.Attack] = UpdAttack;
        
        Init();
	}
	
	void Update () {
		if(nextState != currentState)
        {
            currentState = nextState;
        } else
        {
            states[(int)currentState]();
        }
	}

    // Delegated methods for each possible AI state in the state machine
    void UpdIdle()
    {
        if(Mathf.Abs(target.position.z - transform.position.z) < minimalDistance)
        {

            attackDirection = target.position.z > transform.position.z ? Vector3.back : Vector3.forward;
            timer = timeAttacking;
            nextState = WalkEState.Attack;
        }
    }

    void UpdAttack()
    {
        if(timer <= 0.0f)
        {
            nextState = WalkEState.Idle;
        } else
        {
            transform.Translate(attackDirection * speed);
            timer -= Time.deltaTime;
        }
    }
}
