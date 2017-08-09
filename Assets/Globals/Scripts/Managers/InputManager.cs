using UnityEngine;

// Facade level to handle input from several sources if possible
public static class InputManager
{
    public static bool touchLeft = false;
    public static bool touchRight = false;
    public static bool touchJump = false;

    public static void Init()
    {
        touchLeft = false;
        touchRight = false;
        touchJump = false;
    }

    public static float GetHorizontal()
    {
        float value = Input.GetAxis("Horizontal");
        if(touchLeft)
        {
            value = -1;
        }
        if(touchRight)
        {
            value = 1;
        }
        return value;
    }

    public static float GetVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public static Vector3 GetDirectionInput()
    {
        return new Vector3(0, GetVertical(), GetHorizontal());
    }

    public static bool Jump()
    {
        return Input.GetButtonDown("Jump") || touchJump;
    }

    public static bool StayJump()
    {
        return Input.GetButton("Jump") || touchJump;
    }
}
