using UnityEngine;

public class PlayerController : Entity
{
    public int collectablesAmount;
    public PlayerMachine machine;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        machine = GetComponent<PlayerMachine>();
        collectablesAmount = 0;
        inmunity = false;
        machine.ChangeState("PlayerIdleState");
    }

    public override void OnHit()
    {
        MainManager.Get.gameManager.cameraController.Shake();
        UIManager.Get.hudObject.UpdateHealth((float)currentHitpoints / maxHitpoints);
    }

    public override void Kill()
    {
        MainManager.Get.gameManager.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            CollectableController cc = other.GetComponent<CollectableController>();
            collectablesAmount += cc.scoreValue;
            // Send update to UI
            UIManager.Get.hudObject.UpdateScore(collectablesAmount);
            cc.Collect();
        }
    }
}
