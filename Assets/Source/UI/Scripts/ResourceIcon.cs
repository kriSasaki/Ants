using Source.World.Scripts;
using TMPro;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class ResourceIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public PlayerChecker PlayerChecker => _playerChecker;

        private PlayerChecker _playerChecker;
        private float _neededAmount;
        private float _collectedAmount;

        private void Start()
        {    
            SetAmount();
        }

        public void Initialize(PlayerChecker playerChecker, int amount)
        {
            _neededAmount = amount;
            _playerChecker = playerChecker;
        }
        
        public void ResearchRecources(int amount)
        {
            _collectedAmount = amount;
            SetAmount();
        }

        private void SetAmount() 
        {
            _text.text = $"{_collectedAmount}/{_neededAmount}";
        }
    }
}
