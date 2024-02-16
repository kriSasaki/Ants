using System;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed = 1.5f;

    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;

    private void FixedUpdate()
    {
        Follow();
    }
    
    private void Follow()
    {
        _desiredPosition = _target.position + _offset;
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = _smoothedPosition;
        transform.LookAt(_target);
    }
}