using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public bool HasWeapon => _weapon;
    public int Damage => _damage;
    public event Action<int, int> OnHealthChange;
    public event Action OnPlayerEnable;
    public event Action OnDeath;

    private Weapon _weapon;
    private PlayerAttackState _playerAttackState;
    private AnimationPlayer _animationPlayer;
    private IDetectableObject _detectableObject;
    private int _maxHealth;
    private int _minHealth=0;

    private void Awake()
    {
        _animationPlayer= GetComponent<AnimationPlayer>();
        _detectableObject = GetComponent<IDetectableObject>();
        _playerAttackState = GetComponent<PlayerAttackState>();
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnGameObjectDetectionReleased;
        OnPlayerEnable?.Invoke();
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent -= OnGameObjectDetectionReleased;
    }

    public void GetDamage(int damage)
    {
        _health-=damage;
        _animationPlayer.PlayGetHit();
        OnHealthChange?.Invoke(_health, _maxHealth);

        if(_health <= _minHealth)
        {
            OnDeath?.Invoke();
        }
    }

    public void GetWeapon(Weapon weapon)
    {
        if(weapon != null)
        {
            _weapon = weapon;
            _damage = weapon.Damage;
        }
    }

    public void GetHealth(int health)
    {
        _health = _health + health;
        _maxHealth = _health;
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        if (source.TryGetComponent(out Enemy enemy))
        {
            enemy.Detect();
            _playerAttackState.AddEnemy(enemy.transform);
        }
    }

    private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
    {
        if (source.TryGetComponent(out Enemy enemy))
        {
            enemy.Ignore();
            _playerAttackState.RemoveEnemy();
        }
    }

    public void SpawnWeapon()
    {
        Arm arm = GetComponentInChildren<Arm>();
        Debug.Log(arm);
        Debug.Log(_weapon);
        Instantiate(_weapon.Model, arm.gameObject.transform.position, arm.transform.rotation, arm.transform);
    }
}
