using UnityEngine;

namespace Source.Scripts.UI
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float _rotationX;
        [SerializeField] private float _rotationY;
        [SerializeField] private float _rotationZ;

        private float _xAngle;
        private float _yAngle;
        private float _zAngle;

        private void Update()
        {
            _xAngle = _rotationX * Time.unscaledDeltaTime;
            _yAngle = _rotationY * Time.unscaledDeltaTime;
            _zAngle = _rotationZ * Time.unscaledDeltaTime;

            transform.Rotate(_xAngle, _yAngle, _zAngle);
        }
    }
}