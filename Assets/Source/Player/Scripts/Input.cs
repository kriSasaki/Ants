using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
    private PhysicsMovement _movement;
    private PlayerInput _input;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _movement = GetComponent<PhysicsMovement>();
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _moveDirection = _input.Player.Move.ReadValue<Vector2>();

        _movement.Move(new Vector3(_moveDirection.x, 0, _moveDirection.y));
    }
}
