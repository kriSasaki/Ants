using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _collisionCheckDistance;

    public bool IsMoving => _moveDirection != Vector3.zero;

    private Rigidbody _rigidbody;
    private AnimationPlayer _animationPlayer;
    private PlayerInput _input;
    private Vector3 _moveDirection;
    private float _zero = 0f;
    private float _animationSpeed;
    private float _scaledMoveSpeed;
    private float _scaledRotationSpeed;
    private RaycastHit _hit;
    private Ray _ray;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        if (Application.isMobilePlatform)
        {
            _moveDirection = _joystick.Direction;
        }
        else
        {
            _moveDirection = _input.Player.Move.ReadValue<Vector2>();
        }

        Move(_moveDirection);
    }

    private void Move(Vector3 direction)
    {
        _scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        _scaledRotationSpeed = _rotationSpeed * Time.deltaTime;
        _direction = new Vector3(direction.x, _zero, direction.y);
        _rigidbody.velocity = _direction * _scaledMoveSpeed + new Vector3(_zero, _rigidbody.velocity.y, _zero);
        _animationSpeed = _moveDirection.magnitude;
        _ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.forward);
        Physics.Raycast(_ray, out _hit, _collisionCheckDistance);

        if (_hit.collider == null || _hit.collider.TryGetComponent(out Mushroom mushroom))
        {
            _animationPlayer.SetSpeed(_animationSpeed);
        }
        else
        {
            _animationSpeed = 0;
            _animationPlayer.SetSpeed(_animationSpeed);
        }

        if (_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), _scaledRotationSpeed);
        }
    }
}
