using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
     
    protected float _neededAmount;
    private float _collectedAmount;

    private void Start()
    {
        SetAmount();
    }

    private void OnEnable()
    {
        PlayerChecker.PlayerEnter += SetAmount;
    }

    private void OnDisable()
    {
        PlayerChecker.PlayerEnter -= SetAmount;
    }

    public void SetAmount() 
    {
        _text.text = $"{_collectedAmount}/{_neededAmount}";
    }

    public void SetNeededAmount(int amount)
    {
        _neededAmount = amount;
    }

    protected void ResearchRecources()
    {
        _collectedAmount += 1;
    }
}
