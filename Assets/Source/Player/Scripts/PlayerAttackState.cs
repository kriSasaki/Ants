using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _rotationDuration = 0.2f;

    public bool IsStateActive => _enemies.Count > _noTargets;
    public event Action<int> Attacked;

    private List<Enemy> _enemies;
    private Movement _movement;
    private Player _player;
    private AnimationPlayer _animationPlayer;
    private Tween _tween;
    private int _damage => _player.Damage;
    private readonly int _noTargets = 0;
    private int _enemyIndex;
    private float _lastAttackTime;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _player = GetComponent<Player>();
        _animationPlayer = GetComponent<AnimationPlayer>();
    }

    private void OnEnable()
    {
        _lastAttackTime = _delay;
    }

    private void Update()
    {
        if (_lastAttackTime <= 0 && _movement.IsMoving == false)
        {
            LookAtEnemy(_enemies[^1].transform);
            Attacked?.Invoke(_damage);

            if (_player.HasWeapon)
            {
                _animationPlayer.PlaySwordAttack();
            }
            else
            {
                _animationPlayer.PlayAttack();
            }

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        _enemies[^1].Detect(this);
        _enemies[^1].Dying += RemoveEnemy;

        if (IsStateActive)
        {
            enabled = true;
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemyIndex = _enemies.IndexOf(enemy);
        _enemies[_enemyIndex].Ignore(this);
        _enemies[_enemyIndex].Dying -= RemoveEnemy;
        _enemies.RemoveAt(_enemyIndex);

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