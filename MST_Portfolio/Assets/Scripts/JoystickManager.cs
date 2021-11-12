using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    private static Joystick _joystick;
    protected static float JoystickHorizontal;
    protected static float JoystickVertical;

    private void Awake()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        JoystickHorizontal = _joystick.Horizontal;

        //if (_joystick.Vertical <= 0.0f)
        //{
        //    JoystickVertical = 0;
        //    return;
        //}
        JoystickVertical = _joystick.Vertical;
    }
}