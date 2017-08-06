using UnityEngine;
using System.Collections;

public class PlayerWalkState : BaseState
{
    float currentSpeed;

    public override void Construct(params object[] parameters)
    {
        base.Construct(parameters);
    }

    public override Vector3 ProcessMovement(Vector3 input)
    {
        float speed = MainManager.Get.settings.data.playerSpeed;
        input.Set(0, 0, input.z);
        currentSpeed = Mathf.Min(currentSpeed + 1, speed);
        MotorHelper.FollowVector(ref input, machine.SlopeNormal);
        MotorHelper.KillVector(ref input, machine.WallVector);
        MotorHelper.ApplySpeed(ref input, currentSpeed);
        return input;
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        return base.ProcessRotation(input);
    }

    public override void Transition()
    {
        if(!machine.Grounded())
        {
            machine.ChangeState("PlayerFallState");
        }
        else if(InputManager.Jump())
        {
            machine.ChangeState("PlayerJumpState");
        }
        
        base.Transition();
    }
}
