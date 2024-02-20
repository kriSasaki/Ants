using System.Collections;
using UnityEngine;

public class Egg : Resource
{
    protected override void NoticeResource(Inventory inventory, int amount)
    {
        _coroutine = StartCoroutine(GiveResource(inventory, amount));
    }
    
    private IEnumerator GiveResource(Inventory inventory, int amount)
    {
        _time = 0.1f;
        
        while (_time < _pickUpDuration)
        {
            _time += Time.deltaTime;
            yield return null;
        }
    
        inventory.ChangeEggsAmount(amount);
    }
}