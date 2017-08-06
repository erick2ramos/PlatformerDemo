using UnityEngine;

// Facade level to handle input from several sources if possible
public static class InputManager
{
    public static float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
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
        return Input.GetButtonDown("Jump");
    }

    public static bool StayJump()
    {
        return Input.GetButton("Jump");
    }
}
