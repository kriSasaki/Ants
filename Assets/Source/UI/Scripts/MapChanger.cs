using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [SerializeField] private List<Map> _maps;
    [SerializeField] private MapDisplay _mapDisplay;
    [SerializeField] private GameObject _mapContainer;

    private int _mapIndex;

    private void Start()
    {
        foreach (var map in _maps)
        {
            AddItem(map);
        }
    }

    private void AddItem(Map map)
    {
        var view = Instantiate(_mapDisplay, _mapContainer.transform);
        view.DisplayMap(map);
    }
}
