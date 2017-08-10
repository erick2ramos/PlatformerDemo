using UnityEngine;

public class PlayerMachine : MonoBehaviour
{
    private const float SLOPE_TRESHOLD = 0.55f;
    private const float DISTANCE_GROUNDED = 0.2f;
    private const float INNER_OFFSET_GROUNDED = 0.01f;

    public CharacterController controller;
    public BaseState currentState;

    float gravityBase = 9.8f;
    public float gravityModifier = 0.0f;

    public float Gravity { get { return gravityBase + gravityModifier; } }
    // Directional input with absolute purpose
    public Vector3 InputVector { get; set; }

    // Processed input for velocity and rotation (relative | deltas)
    public Vector3 MoveVector { get; set; }
    public Quaternion RotationQuaternion { get; set; }

    // Collision vector with the normal of side walls
    public Vector3 WallVector { get; set; }
    // Collision vector with the normal of ceilings
    public Vector3 CeilVector { get; set; }
    // Normal of the floor
    public Vector3 SlopeNormal { get; set; }

    public float speed;
    public CollisionFlags ColFlags { set; get; }

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = MainManager.Get.settings.data.playerSpeed;
    }

    void Update()
    {
        UpdateMachine();
    }

    protected virtual void UpdateMachine()
    {
        InputVector = InputManager.GetDirectionInput();

        if(MainManager.Get.settings.gameMode == GameMode.Runner)
        {
            InputVector = new Vector3(0, 0, 1.0f);
        }

        MoveVector = currentState.ProcessMovement(InputVector);
        RotationQuaternion = currentState.ProcessRotation(InputVector);

        Move();
        Rotate();

        currentState.Transition();
    }

    public virtual void Move()
    {
        ColFlags = controller.Move(MoveVector * Time.deltaTime);
        WallVector = (((ColFlags & CollisionFlags.Sides) != 0) || ((ColFlags & CollisionFlags.Below) != 0) ? 
            WallVector : Vector3.zero);
        CeilVector = (((ColFlags & CollisionFlags.Above) != 0) ? CeilVector : Vector3.zero);
    }

    public virtual void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,  RotationQuaternion, 25 * Time.deltaTime);
    }

    // Function to check if state machine owner is currently on the ground
    public virtual bool Grounded()
    {
        float yRay = controller.bounds.center.y - controller.bounds.extents.y + INNER_OFFSET_GROUNDED;
        RaycastHit hit;

        //Raycast at center
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z),
            Vector3.down, out hit, DISTANCE_GROUNDED * 0.5f))
        {
            return (SlopeNormal = hit.normal).y > SLOPE_TRESHOLD;
        }

        // Raycast at front
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay + 0.25f, controller.bounds.center.z + controller.bounds.extents.z - INNER_OFFSET_GROUNDED),
            Vector3.down, out hit, DISTANCE_GROUNDED))
        {
            return (SlopeNormal = hit.normal).y > SLOPE_TRESHOLD;
        }

        // Raycast at back
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay + 0.25f, controller.bounds.center.z - controller.bounds.extents.z + INNER_OFFSET_GROUNDED),
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
        if (Mathf.Abs(hit.normal.y) < 0.7f)
        {
            WallVector = hit.normal;
        }

        if (hit.normal.y < 0)
        {
            CeilVector = hit.normal;
        }
    }
}
