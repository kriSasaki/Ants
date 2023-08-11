using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> MushroomsAmountChanged;
    public Mushroom[] Mushrooms => _mushrooms.ToArray();

    private List<Mushroom> _mushrooms;
    private float _delay;
    private float _timeSecond = 1;

    private void Start()
    {
        _mushrooms= new List<Mushroom>();
    }

    public void AddMushroom(Mushroom mushroom)
    {
        _mushrooms.Add(mushroom);
        MushroomsAmountChanged?.Invoke(_mushrooms.Count);
    }

    public void DeleteMushrooms()
    {
        
    }

    public IEnumerator GiveAwayResources()
    {
        _delay += Time.deltaTime;

         if(_delay == _timeSecond)
        {

        }

        yield return null;
    }

    public void FilterNewItem(GameObject item)
    {
        if(item.TryGetComponent(out Mushroom mushroom))
        {
            AddMushroom(mushroom);
        }
    }
}
