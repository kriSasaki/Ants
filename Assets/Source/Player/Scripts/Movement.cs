using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private AnimationPlayer _animationPlayer;
    private PlayerInput _input;
    private Vector3 _moveDirection;
    private float _zero = 0f;
    private float _scaledMoveSpeed;
    private float _scaledRotationSpeed;

    private void Awake()
    {
        _animationPlayer = GetComponent<AnimationPlayer>();
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        if (Application.isMobilePlatform)
        {
            _joystick.gameObject.SetActive(true);
        }
        else
        {
            _input.Enable();
        }
    }

    private void OnDisable()
    {
        if (Application.isMobilePlatform)
        {
            _joystick.gameObject.SetActive(false);
        }
        else
        {
            _input.Disable();
        }
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            _moveDirection = _joystick.Direction;
        }
        else
        {
            _moveDirection = _input.Player.Move.ReadValue<Vector2>();
        }

        _animationPlayer.SetSpeed(_moveDirection.magnitude);

        Move(_moveDirection);
    }

    private void Move(Vector3 direction)
    {
        _scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        _scaledRotationSpeed = _rotationSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, _zero, direction.y);
        transform.position += moveDirection * _scaledMoveSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion transformPoint = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, transformPoint, _scaledRotationSpeed);
        }
    }
}
