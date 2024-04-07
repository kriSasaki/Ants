using DG.Tweening;
using UnityEngine;

namespace Source.Resources.Scripts
{
    public class DropAnimation : MonoBehaviour
    {
        [SerializeField] private float _maxDeviationX;
        [SerializeField] private float _minDeviationX;
        [SerializeField] private float _maxDeviationZ;
        [SerializeField] private float _minDeviationZ;
        [SerializeField] private float _jumpPower;
        [SerializeField] private int _jumpAmount;
        [SerializeField] private float _duration;
        [SerializeField] private PickUpAnimation _pickUpAnimation;

        private float _deviationX;
        private float _deviationZ;
        private float _time;

        private void Awake()
        {
            _deviationX = Random.Range(_minDeviationX, _maxDeviationX);
            _deviationZ = Random.Range(_minDeviationZ, _maxDeviationZ);
            transform.DOJump(new Vector3(transform.position.x + _deviationX, transform.position.y, transform.position.z + _deviationZ), _jumpPower, _jumpAmount, _duration);
        }

        private void Update()
        {
            _time+= Time.deltaTime;

            if(_time >= _duration)
            {
                _pickUpAnimation.enabled = true;
                Destroy(gameObject, _duration);
            }
        }
    }
}
