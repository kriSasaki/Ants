using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private float _neededAmount;
    private float _collectedAmount;

    private void Start()
    {
        
    }

    public void SetAmount(int amount) 
    {
        _neededAmount = amount;
    }


}
