using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimation : MonoBehaviour
{
    [SerializeField] float _maxDeviationX;
    [SerializeField] float _minDeviationX;
    [SerializeField] float _maxDeviationZ;
    [SerializeField] float _minDeviationZ;
    [SerializeField] float _jumpPower;
    [SerializeField] int _jumpAmount;
    [SerializeField] float _duration;

    private PickUpAnimation _pickUpAnimation;
    private float _deviationX;
    private float _deviationZ;
    private float _time;

    private void Awake()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
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

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
