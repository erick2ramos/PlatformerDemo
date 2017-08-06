using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : MonoBehaviour
{
    private const float SLOPE_TRESHOLD = 0.55f;
    private const float DISTANCE_GROUNDED = 0.2f;
    private const float INNER_OFFSET_GROUNDED = 0.15f;

    public CharacterController controller;
    public BaseState currentState;

    float gravityBase = 9.8f;
    public float gravityModifier = 0.0f;

    public float Gravity { get { return gravityBase + gravityModifier; } }
    public Vector3 InputVector { get; set; }
    public Vector3 MoveVector { get; set; }
    public Quaternion RotationQuaternion { get; set; }
    public Vector3 WallVector { get; set; }
    public Vector3 SlopeNormal { get; set; }

    public CollisionFlags ColFlags { set; get; }

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        ChangeState("PlayerFallState");
    }

    void Update()
    {
        UpdateMachine();
    }

    protected virtual void UpdateMachine()
    {
        InputVector = InputManager.GetDirectionInput();

        MoveVector = currentState.ProcessMovement(InputVector);
        RotationQuaternion = currentState.ProcessRotation(InputVector);

        Move();
        Rotate();

        currentState.Transition();
    }

    public virtual void Move()
    {
        ColFlags = controller.Move(MoveVector * Time.deltaTime);
        WallVector = (((ColFlags & CollisionFlags.Sides) != 0) ? WallVector : Vector3.zero);
    }

    public virtual void Rotate()
    {
        transform.rotation *= RotationQuaternion;
    }

    // Function to check if state machine owner is currently on the ground
    public virtual bool Grounded()
    {
        float yRay = controller.bounds.center.y - controller.bounds.extents.y + INNER_OFFSET_GROUNDED;
        RaycastHit hit;

        Debug.DrawRay(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z), Vector3.down * DISTANCE_GROUNDED, Color.red);
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z),
            Vector3.down, out hit, DISTANCE_GROUNDED))
        {
            return (SlopeNormal = hit.normal).y > SLOPE_TRESHOLD;
        }

        return false;
    }

    public virtual void ChangeState(string stateName, params object[] parameters)
    {
        if (currentState != null)
            currentState.Destruct();
        currentState = (BaseState)GetComponent(stateName);
        currentState.machine = this;
        currentState.Construct(parameters);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Mathf.Abs(hit.normal.y) < 0.2f)
        {
            WallVector = hit.normal;
        }
    }
}
