using System;
using Source.Scripts.Enemies;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private const int MinHealth = 0;
    
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private Weapon.Weapon _weapon;
        [SerializeField] private PlayerAttackState _playerAttackState;
        [SerializeField] private AnimationPlayer _animationPlayer;

        public int MaxHealth { get; private set; }
        public int CurrentHealth => _health;
        public bool HasWeapon => _weapon;
        public int Damage => _damage;
        public event Action<int, int> HealthChanged;
        public event Action PlayerEnabled;
        public event Action Died;
        
        private IDetectableObject _detectableObject;

        private void Awake()
        {
            _detectableObject = GetComponent<IDetectableObject>();
        }

        private void OnEnable()
        {
            _detectableObject.GameObjectDetected += OnGameObjectDetect;
            _detectableObject.GameObjectDetectionReleased += OnGameObjectDetectionReleased;
            PlayerEnabled?.Invoke();
        }

        private void OnDisable()
        {
            _detectableObject.GameObjectDetected -= OnGameObjectDetect;
            _detectableObject.GameObjectDetectionReleased -= OnGameObjectDetectionReleased;
        }

        public void TakeDamage(int damage)
        {
            _health-=damage;
            _animationPlayer.PlayGetHit();
            HealthChanged?.Invoke(_health, MaxHealth);

            if(_health <= MinHealth)
            {
                Died?.Invoke();
            }
        }

        public void Revive(int health)
        {
            _health = health;
            HealthChanged?.Invoke(_health, MaxHealth);
        }

        public void EquipWeapon(Weapon.Weapon weapon)
        {
            if(weapon != null)
            {
                _weapon = weapon;
                _damage = weapon.Damage;
            }
        }

        public void ChangeHealth(int health)
        {
            _health = health;
            MaxHealth = _health;
        }

        private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Enemy enemy))
            {
                _playerAttackState.AddEnemy(enemy);
            }
        }

        private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Enemy enemy))
            {
                _playerAttackState.RemoveEnemy(enemy);
            }
        }

        public void SpawnWeapon()
        {
            Arm arm = GetComponentInChildren<Arm>();
            Instantiate(_weapon.Model, arm.gameObject.transform.position, arm.transform.rotation, arm.transform);
        }
    }
}
