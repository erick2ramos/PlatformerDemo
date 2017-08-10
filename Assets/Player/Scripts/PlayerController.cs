using UnityEngine;

public class PlayerController : Entity
{
    public int collectablesAmount;
    public PlayerMachine machine;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
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

    protected override void Update()
    {
        if(inmunity && hit)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
        } else
        {
            meshRenderer.enabled = true;
        }
        base.Update();
    }

    public override void OnHit()
    {
        machine.ChangeState("PlayerHitState");
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
