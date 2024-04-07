using UnityEngine;

namespace Source.UI.Scripts
{
    public class HealthBar : Bar
    {
        [SerializeField] private Player.Scripts.Player _player;

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
