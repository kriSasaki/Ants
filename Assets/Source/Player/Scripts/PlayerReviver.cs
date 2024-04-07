using UnityEngine;

namespace Source.Player.Scripts
{
    public class PlayerReviver : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public void RevivePlayer()
        {
            if (_player.CurrentHealth <= 0)
            {
                _player.Revive(_player.MaxHealth);
            }
        }
    }
}
