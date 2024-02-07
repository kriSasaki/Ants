using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour
{
    [SerializeField] private List<Map> _maps;
    [SerializeField] private MapDisplay _mapDisplay;
    [SerializeField] private GameObject _mapContainer;

    private LevelService _levelService;
    private List<MapDisplay> _showedMaps;

    private void Awake()
    {
        _levelService = GetComponentInParent<LevelService>();
    }

    private void OnEnable()
    {
        _levelService.OnLevelLoaded += ShowMaps;
    }

    private void OnDisable()
    {
        _levelService.OnLevelLoaded -= ShowMaps;
    }

    public void ChangeButtonsInteractivity(bool isEnable)
    {
        foreach (var map in _showedMaps)
        {
            map.ChangeButtonInteractivity(isEnable);
        }
    }

    private void ShowMaps()
    {
        _showedMaps = new List<MapDisplay>();

        for (int mapIndex = 0; mapIndex < _maps.Count; mapIndex++)
        {
            AddItem(_maps[mapIndex], mapIndex);
        }
    }

    private void AddItem(Map map, int mapIndex)
    {
        _showedMaps.Add(Instantiate(_mapDisplay, _mapContainer.transform));
        _showedMaps[mapIndex].DisplayMap(map);
    }
}
