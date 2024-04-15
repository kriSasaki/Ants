using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class EnemyHealthBar : Bar
    {
        [SerializeField] private Enemy _enemy;

        private void OnEnable()
        {
            _enemy.HealthChanged += ValueChanged;
            ResetToDefault();
        }

        private void OnDisable()
        {
            _enemy.HealthChanged -= ValueChanged;
        }
    }
}
