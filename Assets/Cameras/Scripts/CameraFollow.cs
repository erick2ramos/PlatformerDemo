using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float timeToMove;
    public float offsetZ;
    public float offsetY;

    // Transition fields
    public float gameX;

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

    Queue<Vector3> cameraTrack = new Queue<Vector3>();
    int framesToTrack = 20;

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
        cameraTrack.Clear();
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
        // SKip
    }

    // Moves the camera to gameplay position
    void TransitionUpd()
    {
        Vector3 finalPosition = followTarget.position;
        finalPosition.Set(gameX, (finalPosition.y + offsetY) * 0.5f, finalPosition.z);
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime / timeToMove);
        if (cameraTrack.Count >= framesToTrack)
        {
            cameraTrack.Dequeue();
        }
        cameraTrack.Enqueue(finalPosition);
        if (Mathf.Abs(transform.position.x - finalPosition.x) < 0.1f)
        {
            transform.position = finalPosition;
            nextState = CameraState.PlayMode;
        }
    }

    void PlayModeUpd()
    {
        // Target smooth follow, extrapolating movement
        Vector3 follow = followTarget.position;
        if(shakeTimer <= 0)
        {
            if(cameraTrack.Count >= framesToTrack)
            {
                cameraTrack.Dequeue();
            }
            cameraTrack.Enqueue(follow);
            Vector3[] track = cameraTrack.ToArray();
            follow.z += (track[cameraTrack.Count - 1].z - track[0].z) * offsetZ;
            follow.Set(transform.position.x, (followTarget.position.y + offsetY) * 0.5f, follow.z);
            transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime / timeToMove);
        } else
        {            // Camera shake behaviour
            transform.position += new Vector3(0, Mathf.Log(shakeTimer + 1f) * Mathf.Cos(shakeTimer * 90f), 0);            shakeTimer -= Time.deltaTime;        }
    }

    public void MoveCameraTo(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void ChangeState(CameraState newState)
    {
        nextState = newState;
    }

    public void Shake()
    {
        shakeTimer = shakeTime;    }
}
