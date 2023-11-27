using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public bool HasWeapon => _weapon;
    public int Damage => _damage;
    public event Action<int, int> OnHealthChange;
    public PlayerAttackState PlayerAttackState => _playerAttackState;

    private Weapon _weapon;
    private Appearance _appearance;
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

        if(_weapon != null)
        {
            SpawnWeapon();
        }

        if(_appearance = GetComponentInChildren<Appearance>())
        {
            _health = _health + _appearance.AdditionalHealth;
            _maxHealth = _health;
        }
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
            //
        }
    }

    public void GetWeapon(Weapon weapon)
    {
        if(_weapon != null)
        {
            _weapon.IsEquiped = false;
        }

        _weapon = weapon;
        _weapon.IsEquiped = true;
        _damage = weapon.Damage;
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

    private void SpawnWeapon()
    {
        Arm arm = GetComponentInChildren<Arm>();
        Instantiate(_weapon.WeaponModel, arm.gameObject.transform.position, arm.transform.rotation, arm.transform);
    }
}
