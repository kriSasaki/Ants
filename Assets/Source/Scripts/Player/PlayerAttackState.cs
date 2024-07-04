using System;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAttackState : MonoBehaviour
    {
        private const int NoTargets = 0;

        [SerializeField] private float _delay;
        [SerializeField] private float _rotationDuration = 0.2f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Movement _movement;
        [SerializeField] private Player _player;
        [SerializeField] private AnimationPlayer _animationPlayer;

        private List<Enemy> _enemies;
        private Tween _tween;
        private Vector3 _enemyPosition;
        private int _enemyIndex;
        private float _lastAttackTime;

        public event Action<int> Attacked;

        private bool IsStateActive => _enemies.Count > NoTargets;
        private int Damage => _player.Damage;

        private void Awake()
        {
            _enemies = new List<Enemy>();
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
                Attacked?.Invoke(Damage);
                _audioSource.Play();

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
        }

        private void LookAtEnemy(Transform enemy)
        {
            _enemyPosition = enemy.position;
            _tween = transform.DODynamicLookAt(_enemyPosition, _rotationDuration, AxisConstraint.None, Vector3.up);
        }
    }
}