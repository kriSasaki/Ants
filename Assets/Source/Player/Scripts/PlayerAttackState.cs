using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private float _rotationDuration = 0.2f;

    public bool IsStateActive => _enemies > _noTargets;
    public static event Action<int> Attacked;
    private AnimationPlayer _animationPlayer;
    private Tween _tween;

    private int _enemies = 0;
    private int _noTargets = 0;
    private float _lastAttackTime;

    private void Start()
    {
        _animationPlayer = GetComponent<AnimationPlayer>();
    }

    private void OnEnable()
    {
        _lastAttackTime = _delay;
        Enemy.Dying += RemoveEnemy;
    }

    private void OnDisable()
    {
        Enemy.Dying -= RemoveEnemy;
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attacked?.Invoke(_damage);
            _animationPlayer.PlayAttack();

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public void AddEnemy(Transform enemy)
    {
        _enemies++;

        if (IsStateActive)
        {
            enabled = true;
        }


        LookAtEnemy(enemy);
    }

    public void RemoveEnemy()
    {
        _enemies--;

        if(_enemies<_noTargets)
        {
            _enemies = _noTargets;
        }

        if (IsStateActive == false)
        {
            enabled = false;
        }

        _tween.Kill();
    }

    private void LookAtEnemy(Transform enemy)
    {
        _tween = transform.DODynamicLookAt(enemy.position, _rotationDuration, AxisConstraint.None, Vector3.up);
    }
}
