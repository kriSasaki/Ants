using UnityEngine;

namespace Source.Scripts.Player
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _price;
        [SerializeField] private GameObject _model;
        [SerializeField] private bool _isBought = false;
        [SerializeField] private int _rank;
        [SerializeField] private Color _color;
        
        public int Health => _health;
        public int Price => _price;
        public GameObject Model => _model;
        public bool IsBought => _isBought;
        public int Rank => _rank;
        public Color Color => _color;
    }
}
