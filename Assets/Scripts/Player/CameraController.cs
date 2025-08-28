using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 3, -6);

    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private float _joystickSensitivity = 100f;
    [SerializeField] private float _minYAngle = -25f;
    [SerializeField] private float _maxYAngle = 60f;

    [SerializeField] private PlayerInputSwitcher inputSwitcher;

    private float yaw;   
    private float pitch; 

    private void LateUpdate()
    {
        // знаю что про таймскейл костыль, в более крупном проекте использовал бы отдельный менеджер
        if (_target == null || Time.timeScale == 0f) return;
        InputMode currentInputMode = inputSwitcher.CurrentInputMode;

        Vector2 lookInput = inputSwitcher.GetLookInput();
        float sensitivity = currentInputMode == InputMode.Keyboard ? _mouseSensitivity : _joystickSensitivity;

        yaw += currentInputMode == InputMode.Keyboard ? lookInput.x * sensitivity : lookInput.x * sensitivity * Time.deltaTime;
        pitch -= currentInputMode == InputMode.Keyboard ? lookInput.y * sensitivity : lookInput.y * sensitivity * Time.deltaTime;
        pitch  = Mathf.Clamp(pitch, _minYAngle, _maxYAngle);

        Quaternion rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right); 

        Vector3 desiredPosition = _target.position + rotation * _offset;
        transform.position = desiredPosition;
        transform.LookAt(_target.position);
    }
}
