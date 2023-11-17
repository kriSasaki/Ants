using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string GetDamage = "AntHurt";

    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private Egg _egg;
    [SerializeField] private int _eggsAmount;
    [SerializeField] private Leg _leg;
    [SerializeField] private int _legsAmount;

    public Player Target => _target;
    public int Health => _health;
    public int Zero => _zero;
    public static event Action Dying;
    public event Action<int, int> OnHealthChange;

    private AudioSource _audio;
    private Animator _animator;
    private int _maxHealth;
    private int _zero=0;
    private bool IsDetected = false;

    private void Start()
    {
        _maxHealth = _health;
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerAttackState.Attacked += TakeDamage;
    }

    private void OnDisable()
    {
        PlayerAttackState.Attacked -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        if (IsDetected)
        {
            _health -= damage;
            OnHealthChange?.Invoke(_health, _maxHealth);
            _animator.SetTrigger(GetDamage);
            _audio.Play();

            if (_health <= 0)
            {
                IsDetected = false;
                Dying?.Invoke();
                DropResources();
                Destroy(gameObject, 2);
            }
        }
    }

    public void Detect()
    {
        IsDetected = true;
    }

    public void Ignore()
    {
        IsDetected = false;
    }

    private void DropResources()
    {
        if (_eggsAmount > _zero)
        {
            for (int i = 1; i <= _eggsAmount; i++)
            {
                _egg = Instantiate(_egg, transform.position, Quaternion.identity);
            }
        }

        if (_legsAmount > _zero)
        {
            for (int i = 1; i <= _legsAmount; i++)
            {
                Instantiate(_leg, transform.position, Quaternion.identity);
            }
        }
    }
}
