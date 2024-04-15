using UnityEngine;

namespace Source.Scripts.Player
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
    public class Character : ScriptableObject
    {
        public int Health;
        public int Price;
        public GameObject Model;
        public bool IsBought = false;
        public int Rank;
        public Color Color;
    }
}
