using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _mapName;
    [SerializeField] private Image _mapImage;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _lockIcon;

    [SerializeField] private LevelManager _levelManager;
    private bool _mapUnlocked;
    private int _selectedMap;

    public void DisplayMap(Map map)
    {
        _mapName.text = map.name;
        _mapName.color = map.nameColor;
        _mapImage.sprite = map.mapImage;

        _mapUnlocked = _levelManager.OpenedLevels >= map.mapIndex;
        _lockIcon.SetActive(!_mapUnlocked);
        _playButton.interactable = _mapUnlocked;

        if (_mapUnlocked)
        {
            _mapImage.color = Color.white;
            _selectedMap = map.mapIndex;
        }
        else
        {
            _mapImage.color = Color.gray;
        }
    }

    public void LoadMap()
    {
        _levelManager.LoadLevel(_selectedMap);
    }
}
