using UnityEngine;

public class PlayerHitState : BaseState
{
    float verticalImpulse = 5;
    float speed = 3;
    float vSpeed;

    public override void Construct(params object[] parameters)
    {
        vSpeed = verticalImpulse;
    }

    public override Vector3 ProcessMovement(Vector3 input)
    {
        Vector3 output = new Vector3(0, vSpeed, -input.z * speed);
        vSpeed -= machine.Gravity * Time.deltaTime;
        return output;
    }

    public override void Transition()
    {
        if (vSpeed <= 0)
        {
            machine.ChangeState("PlayerFallState");
        }
    }
}
