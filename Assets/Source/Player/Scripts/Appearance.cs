using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private int _additionalHealth;

    public int AdditionalHealth => _additionalHealth;
}
