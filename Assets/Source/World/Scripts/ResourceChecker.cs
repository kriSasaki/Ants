using UnityEngine;

public class ResourceChecker : MonoBehaviour
{
    [SerializeField] private int _mushrooms;
    [SerializeField] private int _eggs;
    [SerializeField] private int _legs;

    public int Mushrooms => _mushrooms;
    public int Legs => _legs;
    public int Eggs => _eggs;

    public bool ResearchRecources(int mushroomsCollected, int  eggsCollected)
    {
        return mushroomsCollected >= _mushrooms && eggsCollected >= _eggs;
    }
}
