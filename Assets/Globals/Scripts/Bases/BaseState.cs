using UnityEngine;

// Base state class for a behaviour state machine
public class BaseState : MonoBehaviour
{
    public PlayerMachine machine;

    public virtual void Construct(params object[] parameters) { }
    public virtual void Destruct() { }
    public virtual void Transition() { }
    public virtual Vector3 ProcessMovement(Vector3 input) { return input; }
    public virtual Quaternion ProcessRotation(Vector3 input) { return Quaternion.identity; }
}
