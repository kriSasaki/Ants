using System;
using Source.Scripts.Player;
using Source.Scripts.Resources;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour
    {
        private const int Zero = 0;
        private const int DestroyDelay = 2;
        private readonly int _antHurt = Animator.StringToHash("AntHurt");

        [SerializeField] private Player.Player _target;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private Animator _animator;
        [SerializeField] private int _health;
        [SerializeField] private Egg _egg;
        [SerializeField] private int _eggsAmount;

        private Vector3 _position;
        private int _maxHealth;
        private bool _isDetected;

        public event Action<Enemy> Dying;
        public event Action<int, int> HealthChanged;

        public Player.Player Target => _target;
        public int Health => _health;

        private void Start()
        {
            _maxHealth = _health;
        }

        public void Detect(PlayerAttackState playerAttackState)
        {
            playerAttackState.Attacked += TakeDamage;
            _isDetected = true;
        }

        public void Ignore(PlayerAttackState playerAttackState)
        {
            playerAttackState.Attacked -= TakeDamage;
            _isDetected = false;
        }

        private void TakeDamage(int damage)
        {
            if (_isDetected)
            {
                _health -= damage;
                HealthChanged?.Invoke(_health, _maxHealth);
                _animator.SetTrigger(_antHurt);
                _audio.Play();

                if (_health <= Zero)
                {
                    _isDetected = false;
                    Dying?.Invoke(this);
                    DropResources();
                    Destroy(gameObject, DestroyDelay);
                }
            }
        }

        private void DropResources()
        {
            if (_eggsAmount > Zero)
            {
                for (var i = 1; i <= _eggsAmount; i++)
                {
                    _egg = Instantiate(_egg, transform.position, Quaternion.identity);
                }
            }
        }
    }
}