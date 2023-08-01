using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private SurfaceSlider _surfaceSlider;
    private Vector3 _directionAlongSurface;
    private Vector3 _offset;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _surfaceSlider = GetComponent<SurfaceSlider>();
    }

    public void Move(Vector3 direction)
    {
        _directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        _offset = _directionAlongSurface * (_speed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + _offset);
    }
}
