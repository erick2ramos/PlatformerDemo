using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEController : BaseEnemy {
    // ShootE state machine fields
    EnemyState[] states;
    public enum ShootE
    {
        Idle,
        Attack,
        Count
    };
    public ShootE currentState;
    public ShootE nextState;

    // Bullet fields
    public GameObject bulletPrefab;
    public GameObject[] bullets;
    public int maxBullets;
    public Transform shootPoint;

    public override void Init()
    {
        base.Init();
        nextState = ShootE.Idle;
    }
    
	void Start () {
        // Initializing state machine
        states = new EnemyState[(int)ShootE.Count];
        states[(int)ShootE.Idle] = UpdIdle;
        states[(int)ShootE.Attack] = UpdAttack;

        // Instatiating bullets to shoot
        bullets = new GameObject[maxBullets];
        for (int i = 0; i < maxBullets; i++)
        {
            bullets[i] = Instantiate(bulletPrefab);
            bullets[i].SetActive(false);
        }

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

            attackDirection = target.position.z < transform.position.z ? Vector3.back : Vector3.forward;
            Shoot(attackDirection);
            timer = timeAttacking;
            nextState = ShootE.Attack;
        }
    }

    void UpdAttack()
    {
        if(timer <= 0.0f)
        {
            nextState = ShootE.Idle;
        } else
        {
            timer -= Time.deltaTime;
        }
    }
    //

    public void Shoot(Vector3 direction)
    {
        // Shoots the first bullet, not shot already, in the param direction
        for (int i = 0; i < maxBullets; i++)
        {
            if(!bullets[i].activeSelf)
            {
                bullets[i].GetComponent<BulletController>().Shoot(shootPoint.position, direction);
                break;
            }
        }
    }
}
