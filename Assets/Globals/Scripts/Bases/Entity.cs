using UnityEngine;

// Base class for player and enemies
public class Entity : MonoBehaviour
{
    public int maxHitpoints = 5;
    public int currentHitpoints;

    public bool inmunity;
    public float inmunityTime;
    protected float timer;
    public bool hit;

    public bool IsAlive { get { return currentHitpoints > 0; } }

    public virtual void Init()
    {
        inmunity = false;
        maxHitpoints = MainManager.Get.settings.data.playerMaxLife;
        currentHitpoints = maxHitpoints;
    }

    protected virtual void Update()
    {
        if (hit)
        {
            if(timer <= 0)
            {
                inmunity = false;
                hit = false;
            }
            timer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        if (inmunity)
            return;
        inmunity = true;
        hit = true;
        timer = inmunityTime;
        currentHitpoints = Mathf.Clamp(currentHitpoints - amount, 0, maxHitpoints);
        OnHit();
        if (!IsAlive)
        {
            Kill();
        }
    }

    // React to a hit
    public virtual void OnHit() { }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
