using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibration;
    [SerializeField] private float _shakeRandomness;

    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider= GetComponent<SphereCollider>();
    }

    public void JumpIn(Vector3 playerPosition)
    {
        _sphereCollider.enabled = false;
        transform.DOJump(playerPosition, _jumpPower, _jumpCount, _jumpDuration);
        transform.DOShakeRotation(_shakeDuration, _shakeStrength, _shakeVibration, _shakeRandomness);
        Destroy(gameObject, _jumpDuration);
    }
}
