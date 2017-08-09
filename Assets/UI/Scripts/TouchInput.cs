using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public void TouchLeft(bool value)
    {
        InputManager.touchLeft = value;
    }

    public void TouchRight(bool value)
    {
        InputManager.touchRight = value;
    }

    public void TouchJump(bool value)
    {
        InputManager.touchJump = value;
    }
}
