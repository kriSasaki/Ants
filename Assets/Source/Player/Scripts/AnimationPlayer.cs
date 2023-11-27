using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string Attack = "Attack";
    private const string SwordAttack = "SwordAttack";
    private const string GetHit = "GetHit";

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

    public void PlayAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void PlaySwordAttack()
    {
        _animator.SetTrigger(SwordAttack);
    }

    public void PlayGetHit()
    {
        _animator.SetTrigger(GetHit);
    }
}
