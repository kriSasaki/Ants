using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayer : MonoBehaviour
{
    private CharacterManager _characterManager;
    private List<GameObject> _appearances;
    private int _currentAppearance;
    private int _layerUI = 5;

    private void Awake()
    {
        _characterManager = GetComponentInParent<CharacterManager>();
    }

    private void Start()
    {
        _appearances = new List<GameObject>();
        SpawnAppearances();
        ChangeAppearanceActivity(_appearances[_characterManager.CurentCharacter], true);
    }

    private void OnEnable()
    {
        _characterManager.OnAppearanceChange += ChangeAppearance;
    }

    private void OnDisable()
    {
        _characterManager.OnAppearanceChange -= ChangeAppearance;
    }


    private void SpawnAppearances()
    {
        for (int appearanceNumber = 0; appearanceNumber < _characterManager.AppearanceAmount; appearanceNumber++)
        {               
            _appearances.Add(_characterManager.GetAppearance(appearanceNumber));
           _appearances[appearanceNumber] = Instantiate(_appearances[appearanceNumber], transform.position, transform.rotation, transform);
            ChangeLayer(_appearances[appearanceNumber], _layerUI);
            ChangeScale(_appearances[appearanceNumber], 130);
            ChangeAppearanceActivity(_appearances[appearanceNumber], false);
        }
    }

    private void ChangeAppearance()
    {
        ChangeAppearanceActivity(_appearances[_currentAppearance], false);
        ChangeAppearanceActivity(_appearances[_characterManager.CurentCharacter], true);
        _currentAppearance = _characterManager.CurentCharacter;
    }

    private void ChangeAppearanceActivity(GameObject appearance, bool isNeedChange)
    {
        appearance.gameObject.SetActive(isNeedChange);
    }

    private void ChangeLayer(GameObject appearance, int layer)
    {
        appearance.gameObject.layer = layer;

        foreach (Transform item in appearance.transform)
        {
            item.gameObject.layer = layer;
        }
    }

    private void ChangeScale(GameObject appearance, float scale)
    {
        appearance.transform.localScale = new Vector3(scale, scale, scale);
    }
}
