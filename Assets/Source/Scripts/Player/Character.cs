using UnityEngine;

namespace Source.Scripts.Player
{
    public class Character
    {
        public Character(CharacterConfig characterConfig)
        {
            Health = characterConfig.Health;
            Price = characterConfig.Price;
            Model = characterConfig.Model;
            IsBought = characterConfig.IsBought;
            Rank = characterConfig.Rank;
            Color = characterConfig.Color;
        }

        public int Health { get; private set; }
        public int Price { get; private set; }
        public GameObject Model { get; private set; }
        public bool IsBought { get; private set; }
        public int Rank { get; private set; }
        public Color Color { get; private set; }

        public void BuyItem()
        {
            IsBought = true;
        }
    }
}