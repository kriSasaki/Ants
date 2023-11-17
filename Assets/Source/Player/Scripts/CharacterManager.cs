using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _appearances;

    public int AppearanceAmount => _appearances.Count;
    public int CurentCharacter { get; private set; }

    public GameObject GetAppearance(int appearanceNumber)
    {
        return _appearances[appearanceNumber];
    }
}
