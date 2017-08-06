using UnityEngine;
using System.Collections;

public class PlayerIdleState : BaseState
{
    public override Vector3 ProcessMovement(Vector3 input)
    {
        return Vector3.zero;
    }

    public override void Transition()
    {
        
    }
}
