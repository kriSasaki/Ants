using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _model;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private int _rank;

    public int Damage => _damage;
    public int Price => _price;
    public GameObject Model => _model;
    public bool IsBuyed => _isBuyed;
    public int Rank => _rank;

    public void BuyItem()
    {
        _isBuyed = true;
    }
}
