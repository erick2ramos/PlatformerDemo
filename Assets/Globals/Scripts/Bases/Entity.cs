using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for player and enemies
public class Entity : MonoBehaviour
{
    public int maxHitpoints = 5;
    public int currentHitpoints;

    public bool inmunity;
    public float inmunityTime;
    float timer;

    public bool IsAlive { get { return currentHitpoints > 0; } }

    public virtual void Init()
    {
        maxHitpoints = MainManager.Get.settings.data.playerMaxLife;
        currentHitpoints = maxHitpoints;
    }

    public void TakeDamage(int amount)
    {
        currentHitpoints = Mathf.Clamp(currentHitpoints - amount, 0, maxHitpoints);
        OnHit();
        if (!IsAlive)
        {
            Kill();
        }
        print("hit");
    }

    // React to a hit
    public virtual void OnHit() { }

    public virtual void Kill()
    {
        print("BOom");
        Destroy(gameObject);
    }
}
