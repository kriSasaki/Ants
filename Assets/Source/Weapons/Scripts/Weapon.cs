using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public int Damage;
    public int Price;
    public GameObject WeaponModel;
    public bool IsBuyed = false;
    public int WeaponRank;
}
