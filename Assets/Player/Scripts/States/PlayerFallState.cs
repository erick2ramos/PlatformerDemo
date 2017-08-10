using UnityEngine;

public class PlayerFallState : BaseState
{
    public float terminalVelocity = 25.0f;
    float currentFallingSpeed;

    public override void Construct(params object[] parameters)
    {
        currentFallingSpeed = 0.0f;
        if(parameters.Length > 0)
        {
            currentFallingSpeed = (float)parameters[0];
        }
        base.Construct(parameters);
    }

    public override Vector3 ProcessMovement(Vector3 input)
    {
        MotorHelper.KillVector(ref input, machine.WallVector);
        MotorHelper.ApplySpeed(ref input, machine.speed);
        MotorHelper.ApplyGravity(ref input, ref currentFallingSpeed, machine.Gravity, terminalVelocity);
        return input;
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        input.Set(0, 0, input.z);
        if (input.sqrMagnitude == 0) { return base.ProcessRotation(input); }
        return Quaternion.LookRotation(input);
    }

    public override void Transition()
    {
        if(machine.Grounded())
        {
            machine.ChangeState("PlayerWalkState");
        }
    }
}
