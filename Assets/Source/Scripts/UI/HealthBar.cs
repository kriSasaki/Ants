using UnityEngine;

namespace Source.Scripts.UI
{
    public class HealthBar : Bar
    {
        [SerializeField] private Player.Player _player;

        private void OnEnable()
        {
            _player.HealthChanged += ValueChanged;
            ResetToDefault();
        }

        private void OnDisable()
        {
            _player.HealthChanged -= ValueChanged;
        }
    }
}