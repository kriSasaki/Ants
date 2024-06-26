using Source.Scripts.World;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ResourceIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private PlayerChecker _playerChecker;
        private float _neededAmount;
        private float _collectedAmount;

        public PlayerChecker PlayerChecker => _playerChecker;

        private void Start()
        {
            SetAmount();
        }

        public void Initialize(PlayerChecker playerChecker, int amount)
        {
            _neededAmount = amount;
            _playerChecker = playerChecker;
        }

        public void ResearchResources(int amount)
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