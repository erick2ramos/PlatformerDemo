using UnityEngine;
using System.Collections;

public class PlayerJumpState : BaseState
{
    public float minimalJumpMomentum;
    public float jumpForce;
    float currentJumpForce;

    public override void Construct(params object[] parameters)
    {
        jumpForce = MainManager.Get.settings.data.playerJumpForce;
        currentJumpForce = jumpForce;
        base.Construct(parameters);
    }

    public override Vector3 ProcessMovement(Vector3 input)
    {
        MotorHelper.KillVector(ref input, machine.WallVector);
        MotorHelper.ApplySpeed(ref input, machine.speed);
        input.Set(0, currentJumpForce, input.z);
        currentJumpForce -= (machine.Gravity * Time.deltaTime);

        return base.ProcessMovement(input);
    }

    public override void Transition()
    {
        if (machine.CeilVector.y < 0)
        {
            machine.ChangeState("PlayerFallState");
        }

        if (currentJumpForce < minimalJumpMomentum || (jumpForce - currentJumpForce > minimalJumpMomentum && !InputManager.StayJump()))
        {
            machine.ChangeState("PlayerFallState", 2.0f);
        }
    }
}
