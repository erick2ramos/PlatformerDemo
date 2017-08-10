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

    public void Update()
    {
        if(MainManager.Get.settings.gameMode == GameMode.Runner)
        {
            InputManager.touchJump = Input.GetButtonDown("Fire1") || Input.GetButton("Fire1");
        }
    }
}
