using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string GetDamage = "AntHurt";

    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private Egg _egg;
    [SerializeField] private int _eggsAmount;

    public Player Target => _target;
    public int Health => _health;
    public int Zero => _zero;
    public static event Action Dying;
    public event Action<int, int> OnHealthChange;

    //private PlayerChecker _playerChecker;
    private AudioSource _audio;
    private Animator _animator;
    private Vector3 _position;
    private int _maxHealth;
    private int _zero = 0;
    private bool IsDetected = false;

    private void Start()
    {
        //_playerChecker = GetComponentInParent<PlayerCheckerTransmitter>().PlayerChecker;
        //_position = transform.position;
        //transform.position = new Vector3(transform.position.x, -15, transform.position.z);
        _maxHealth = _health;
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerAttackState.Attacked += TakeDamage;
        //_playerChecker.ConditionIsDone += Show;
    }

    private void OnDisable()
    {
        PlayerAttackState.Attacked -= TakeDamage;
        //_playerChecker.ConditionIsDone -= Show;
    }

    public void Detect()
    {
        IsDetected = true;
    }

    public void Ignore()
    {
        IsDetected = false;
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

    private void DropResources()
    {
        if (_eggsAmount > _zero)
        {
            for (int i = 1; i <= _eggsAmount; i++)
            {
                _egg = Instantiate(_egg, transform.position, Quaternion.identity);
            }
        }
    }

    //private void Show()
    //{
    //    transform.position = _position;
    //}
}
