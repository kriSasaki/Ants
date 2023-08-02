using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private List<Mushroom> _mushrooms;
    public event UnityAction<int> MushroomsAmountChanged;


    private void Start()
    {
        _mushrooms= new List<Mushroom>();
    }

    public void AddMushroom(Mushroom mushroom)
    {
        _mushrooms.Add(mushroom);
        MushroomsAmountChanged?.Invoke(_mushrooms.Count);
    }
}
