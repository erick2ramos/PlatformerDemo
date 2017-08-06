using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for player and enemies
public class Entity : MonoBehaviour
{
    public int maxHitpoints = 5;
    public int currentHitpoints;

    public bool IsAlive { get { return currentHitpoints > 0; } }

    public void TakeDamage(int amount)
    {
        currentHitpoints = Mathf.Clamp(currentHitpoints - amount, 0, maxHitpoints);
        if (!IsAlive)
        {
            Kill();
        }
    }

    public void Heal(int amount)
    {
        currentHitpoints = Mathf.Clamp(currentHitpoints + amount, 0, maxHitpoints);
    }

    public virtual void Reboot()
    {
        maxHitpoints = MainManager.Get.settings.data.playerMaxLife;
        currentHitpoints = maxHitpoints;
    }

    public virtual void Kill()
    {
        print("BOom");
        Destroy(gameObject);
    }
}
