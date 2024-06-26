using System.Collections.Generic;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class MapChanger : MonoBehaviour
    {
        [SerializeField] private List<Map> _maps;
        [SerializeField] private MapDisplay _mapDisplay;
        [SerializeField] private GameObject _mapContainer;
        [SerializeField] private LevelService _levelService;

        private List<MapDisplay> _showedMaps;

        private void OnEnable()
        {
            _levelService.OnLevelLoaded += ShowMaps;
        }

        private void OnDisable()
        {
            _levelService.OnLevelLoaded -= ShowMaps;
        }

        private void ShowMaps()
        {
            _showedMaps = new List<MapDisplay>();

            for (var mapIndex = 0; mapIndex < _maps.Count; mapIndex++)
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
}