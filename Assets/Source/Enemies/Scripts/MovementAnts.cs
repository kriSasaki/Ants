using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnts : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private AnimationAnts _animationAnts;
    private Vector3 _moveDirection;
    private float _zero = 0f;
    private float _scaledMoveSpeed;
    private float _scaledRotationSpeed;

    private void Start()
    {
        _animationAnts= GetComponent<AnimationAnts>();
    }

    private void Update()
    {
        SetMoveDirection();

        Move();
    }

    private void SetMoveDirection()
    {
        _moveDirection = transform.position - _target.position;
        _moveDirection.Normalize();
    }

    private void Move()
    {
        _scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        _scaledRotationSpeed = _rotationSpeed * Time.deltaTime;

        transform.position += -_moveDirection * _scaledMoveSpeed;

        if (_moveDirection != Vector3.zero)
        {
            Quaternion transformPoint = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, transformPoint, _scaledRotationSpeed);
        }
    }
}
