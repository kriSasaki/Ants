using UnityEngine;

namespace Source.Player.Scripts
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
    public class Character : ScriptableObject
    {
        public int Health;
        public int Price;
        public GameObject Model;
        public bool IsBuyed = false;
        public int Rank;
        public Color Color;
    }
}
