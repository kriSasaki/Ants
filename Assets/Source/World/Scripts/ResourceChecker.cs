using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChecker : MonoBehaviour
{
    [SerializeField] private int _mushrooms;
    [SerializeField] private int _legs;

    public int Mushrooms => _mushrooms;
    public int Legs => _legs;

    public bool CheckResources(Player player) 
    {
        return _mushrooms == player.Mushrooms.Length;
    }
}
