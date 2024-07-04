using Source.Scripts.Resources;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        private const float Zero = 0f;
        private const float RayCastLaunchY = 0.5f;

        [SerializeField] private Joystick.Base.Joystick _joystick;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _collisionCheckDistance;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationPlayer _animationPlayer;

        private PlayerInput _input;
        private Vector3 _moveDirection;
        private float _animationSpeed;
        private float _scaledMoveSpeed;
        private float _scaledRotationSpeed;
        private RaycastHit _hit;
        private Ray _ray;
        private Vector3 _direction;
        private Vector3 _velocityDirection;
        private Vector3 _rayDirection;

        public bool IsMoving => _moveDirection != Vector3.zero;

        private void Awake()
        {
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
            _direction = new Vector3(direction.x, Zero, direction.y);
            _velocityDirection = new Vector3(Zero, _rigidbody.velocity.y, Zero);
            _rigidbody.velocity = _direction * _scaledMoveSpeed + _velocityDirection;
            _animationSpeed = _moveDirection.magnitude;
            _rayDirection = new Vector3(Zero, RayCastLaunchY, Zero);
            _ray = new Ray(transform.position + _rayDirection, transform.forward);
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
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.LookRotation(_direction),
                    _scaledRotationSpeed);
            }
        }
    }
}