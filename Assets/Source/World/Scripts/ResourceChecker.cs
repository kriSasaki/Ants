using UnityEngine;

namespace Source.World.Scripts
{
    public class ResourceChecker : MonoBehaviour
    {
        [SerializeField] private int _mushrooms;
        [SerializeField] private int _eggs;

        public int Mushrooms => _mushrooms;
        public int Eggs => _eggs;

        public bool ResearchRecources(int mushroomsCollected, int  eggsCollected)
        {
            return mushroomsCollected >= _mushrooms && eggsCollected >= _eggs;
        }
    }
}
