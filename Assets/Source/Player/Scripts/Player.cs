using System;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public event Action<int, int> OnHealthChange;
    public PlayerAttackState PlayerAttackState => _playerAttackState;

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
        _maxHealth = _health;
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnGameObjectDetectionReleased;
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
}
