using UnityEngine;

namespace Source.Weapons.Scripts
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _price;
        [SerializeField] private GameObject _model;
        [SerializeField] private bool _isBuyed = false;
        [SerializeField] private int _rank;
        [SerializeField] private Color _color;

        public int Damage => _damage;
        public int Price => _price;
        public GameObject Model => _model;
        public bool IsBuyed => _isBuyed;
        public int Rank => _rank;
        public Color Color => _color;

        public void BuyItem()
        {
            _isBuyed = true;
        }
    }
}
