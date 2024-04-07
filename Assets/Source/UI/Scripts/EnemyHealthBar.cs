using Source.Enemies.Scripts;
using UnityEngine;

namespace Source.UI.Scripts
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
