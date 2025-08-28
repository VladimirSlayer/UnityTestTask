using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _movementBoundaries = 12.5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private PlayerInputSwitcher inputSwitcher;

    private Camera mainCamera;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 velocity = CalculateVelocity();
        ApplyGravity(ref velocity);
        controller.Move(velocity * Time.deltaTime);
        CheckMovementBoundaries();
    }

    private void ApplyGravity(ref Vector3 velocity)
    {
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;
        else
            verticalVelocity += _gravity * Time.deltaTime;

        velocity.y = verticalVelocity;
    }

    public void ApplySpeedBonus(float multiplier, float duration)
    {
        StartCoroutine(SpeedBonusCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBonusCoroutine(float multiplier, float duration)
    {
        float originalSpeed = _playerSpeed;
        _playerSpeed *= multiplier;

        yield return new WaitForSeconds(duration);
        _playerSpeed = originalSpeed;
    }

    private Vector3 CalculateVelocity()
    {
        Vector2 move = inputSwitcher.GetMovementInput();

        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        return (camForward * move.y + camRight * move.x) * _playerSpeed;
    }

    private void CheckMovementBoundaries()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -_movementBoundaries, _movementBoundaries);
        pos.z = Mathf.Clamp(pos.z, -_movementBoundaries, _movementBoundaries);
        transform.position = pos;
    }
}
