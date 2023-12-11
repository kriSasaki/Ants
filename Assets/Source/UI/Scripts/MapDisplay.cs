using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _mapName;
    [SerializeField] private Image _mapImage;
    [SerializeField] private GameObject _lockIcon;
    [SerializeField] private GameObject _checkIcon;

    private Button _chooseButton;
    private LevelManager _levelManager;
    private bool _mapUnlocked;
    private bool _mapSelected;
    private int _selectedMap;

    private void Awake()
    {
        _chooseButton = GetComponent<Button>();
        _levelManager = GetComponentInParent<LevelManager>();
    }

    public void DisplayMap(Map map)
    {
        _mapName.text = map.mapName;
        _mapName.color = map.nameColor;
        _mapImage.sprite = map.mapImage;

        _mapUnlocked = _levelManager.OpenedLevels >= map.mapIndex;
        _lockIcon.SetActive(!_mapUnlocked);
        _chooseButton.enabled = _mapUnlocked;

        if (_mapUnlocked)
        {
            _mapImage.color = Color.white;
            _selectedMap = map.mapIndex;
        }
        else
        {
            _mapImage.color = Color.gray;
        }

        _mapSelected = _levelManager.CurrentLevel == map.mapIndex;
        _chooseButton.enabled = !_mapSelected && _mapUnlocked;
        _checkIcon.SetActive(_mapSelected);
    }

    public void ChangeButtonInteractivity(bool isEnable)
    {
        _chooseButton.enabled = isEnable;
    }

    public void ChooseMap()
    {
        _levelManager.LoadLevel(_selectedMap);
    }
}
