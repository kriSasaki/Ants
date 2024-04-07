using Source.Player.Scripts;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class PlayerTransmitter : MonoBehaviour
    {
        [SerializeField] private Player.Scripts.Player _player;
        [SerializeField] private Inventory _inventory;

        public Player.Scripts.Player Player => _player;
        public Inventory Inventory => _inventory;
    }
}
