using UnityEngine;

namespace Source.Scripts.World
{
    public class ResourceChecker : MonoBehaviour
    {
        [SerializeField] private int _mushrooms;
        [SerializeField] private int _eggs;

        public int Mushrooms => _mushrooms;
        public int Eggs => _eggs;

        public bool ResearchResources(int mushroomsCollected, int  eggsCollected)
        {
            return mushroomsCollected >= _mushrooms && eggsCollected >= _eggs;
        }
    }
}
