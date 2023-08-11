using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableObjectReactionPickUp : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibration;
    [SerializeField] private float _shakeRandomness;

    private SphereCollider _sphereCollider;
    private IDetectableObject _detectableObject;

    private void Awake()
    {
        _detectableObject = GetComponent<IDetectableObject>();     
    }

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;    
    }

    public void JumpIn(Vector3 playerPosition)
    {
        _sphereCollider.enabled = false;
        transform.DOJump(playerPosition, _jumpPower, _jumpCount, _jumpDuration);
        transform.DOShakeRotation(_shakeDuration, _shakeStrength, _shakeVibration, _shakeRandomness);
        Destroy(gameObject, _jumpDuration);
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        if (source.TryGetComponent(out Player player))
        {
            player.FilterNewItem(detectedObject);
            JumpIn(source.transform.position);
        }
    }
}
