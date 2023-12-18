using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    protected PlayerChecker _playerChecker;
    protected float _neededAmount;
    private float _collectedAmount;

    private void Awake()
    {
        _playerChecker = GetComponentInParent<PlayerChecker>();
    }

    private void Start()
    {    
        SetAmount();
    }

    public void SetNeededAmount(int amount)
    {
        _neededAmount = amount;
    }

    protected void SetAmount() 
    {
        _text.text = $"{_collectedAmount}/{_neededAmount}";
    }

    protected void ResearchRecources(int amount)
    {
        _collectedAmount = amount;
        SetAmount();
    }
}
