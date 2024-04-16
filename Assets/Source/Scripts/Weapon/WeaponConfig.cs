using UnityEngine;

namespace Source.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _price;
        [SerializeField] private GameObject _model;
        [SerializeField] private bool _isBought = false;
        [SerializeField] private int _rank;
        [SerializeField] private Color _color;

        public int Damage => _damage;
        public int Price => _price;
        public GameObject Model => _model;
        public bool IsBought => _isBought;
        public int Rank => _rank;
        public Color Color => _color;
    }
}
