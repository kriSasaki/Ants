using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private const string Speed = "Speed";

    private Animator _animator;

    private float _speed;
    private bool _isRunning;
    private bool _isAttacking;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        _animator.SetFloat(Speed, speed);
    }
}
