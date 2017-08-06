using UnityEngine;

public class PlayerController : Entity
{
    public int collectablesAmount;
    public PlayerMachine machine;

    private void Start()
    {
        machine = GetComponent<PlayerMachine>();
        collectablesAmount = 0;
        Init();
    }

    public override void OnHit()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            CollectableController cc = other.GetComponent<CollectableController>();
            collectablesAmount += cc.scoreValue;
            // Send update to UI
            cc.Collect();
        }
    }
}
