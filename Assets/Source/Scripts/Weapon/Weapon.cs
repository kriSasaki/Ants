using UnityEngine;

namespace Source.Scripts.Weapon
{
    public class Weapon
    {
        public Weapon(WeaponConfig weaponConfig)
        {
            Damage = weaponConfig.Damage;
            Price = weaponConfig.Price;
            Model = weaponConfig.Model;
            IsBought = weaponConfig.IsBought;
            Rank = weaponConfig.Rank;
            Color = weaponConfig.Color;
        }

        public int Damage { get; private set; }
        public int Price { get; private set; }
        public GameObject Model { get; private set; }
        public bool IsBought { get; private set; } = false;
        public int Rank { get; private set; }
        public Color Color { get; private set; }

        public void BuyItem()
        {
            IsBought = true;
        }
    }
}