using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform followTarget;
    public float timeToMove;
    public float offsetZ;
    public float offsetY;
    public bool isChangingPosition;

    Vector3 finalPosition;

	void Start () {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () {
        Vector3 follow = followTarget.position;
        float averageY = (follow.y + offsetY) / 2;
        follow.Set(transform.position.x, averageY, follow.z + offsetZ);
        finalPosition = follow;
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime / timeToMove);
	}

    public void MoveCameraTo(Vector3 newPos)
    {
        finalPosition = newPos;
        isChangingPosition = true;
    }
}
