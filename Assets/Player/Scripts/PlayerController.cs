using UnityEngine;

public class PlayerController : Entity
{
    public int collectablesAmount;

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
