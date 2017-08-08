using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float timeToMove;
    public float offsetZ;
    public float offsetY;
    public bool isChangingPosition;

    // Transition fields
    public float startingX;
    public float gameX;

    Vector3 finalPosition;

    // Camera state machine fields
    delegate void StateUpdate();
    StateUpdate[] states;
    public enum CameraState
    {
        MainMenu,
        Transition,
        PlayMode,
        Count
    }
    CameraState currentState;
    public CameraState nextState;

    // Camera shake fields
    public float shakeTime;
    float shakeTimer;

    // Camera sway fields

    void Start()
    {
        //Initializing state machine
        states = new StateUpdate[(int)CameraState.Count];
        states[(int)CameraState.MainMenu] = MainMenuUpd;
        states[(int)CameraState.Transition] = TransitionUpd;
        states[(int)CameraState.PlayMode] = PlayModeUpd;

    }

    public void Init()
    {
        followTarget = MainManager.Get.gameManager.player.transform;
    }

    private void Update()
    {
        if (currentState != nextState)
        {
            currentState = nextState;
        }
        states[(int)currentState]();
    }

    // States functions
    void MainMenuUpd()
    {
        // Do camera sway close to follow target
    }

    void TransitionUpd()
    {
        Vector3 finalPosition = followTarget.position;
        finalPosition.Set(gameX, (finalPosition.y + offsetY) * 0.5f, finalPosition.z + offsetZ);
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime / timeToMove);
        if(Vector3.Distance(transform.position, finalPosition) < 0.1f)
        {
            transform.position = finalPosition;
            nextState = CameraState.PlayMode;
        }
    }

    void PlayModeUpd()
    {
        // Target smooth follow
        Vector3 follow = followTarget.position;
        float averageY = (follow.y + offsetY) / 2;
        follow.Set(transform.position.x, averageY, follow.z + offsetZ);
        finalPosition = follow;
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime / timeToMove);

        // Camera shake behaviour
        if (shakeTimer > 0)        {            transform.position += new Vector3(Mathf.Log(shakeTimer + 1f) * Mathf.Cos(shakeTimer * 90f), 0, 0);            shakeTimer -= Time.deltaTime;        }
    }
    //

    public void MoveCameraTo(Vector3 newPos)
    {
        finalPosition = newPos;
    }

    public void ChangeState(CameraState newState)
    {
        nextState = newState;
    }

    void Sway()
    {

    }

    public void Shake()
    {
        shakeTime = shakeTimer;    }
}
