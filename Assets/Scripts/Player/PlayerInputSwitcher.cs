using UnityEngine;

public enum InputMode { Keyboard, Joystick }

public class PlayerInputSwitcher : MonoBehaviour
{
    public InputMode CurrentInputMode { get; private set; } = InputMode.Keyboard;

    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private Joystick _lookJoystick;

    private void Update()
    {
        CurrentInputMode = DetectInputMode();
    }

    private InputMode DetectInputMode()
    {
        if (DetectJoystickInput()) return InputMode.Joystick;
        if (DetectKeyboardInput()) return InputMode.Keyboard;

        return CurrentInputMode;
    }

    private bool DetectKeyboardInput()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f
               || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f
               || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f
               || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f;
    }

    private bool DetectJoystickInput()
    {
        return (_movementJoystick != null && _movementJoystick.Direction.magnitude > 0.1f)
            || (_lookJoystick != null && _lookJoystick.Direction.magnitude > 0.1f);
    }

    public Vector2 GetMovementInput()
    {
        return CurrentInputMode == InputMode.Keyboard
            ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
            : (_movementJoystick != null ? _movementJoystick.Direction : Vector2.zero);
    }

    public Vector2 GetLookInput()
    {
        return CurrentInputMode == InputMode.Keyboard
            ? new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))
            : (_lookJoystick != null ? _lookJoystick.Direction : Vector2.zero);
    }
}
