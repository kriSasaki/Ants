using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using IJunior.TypedScenes;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    public int OpenedLevels {  get; private set; }
    public int CurrentLevel => _currentLevel;

    private SceneLoadHandler _sceneLoadHandler;
    private int _currentLevel = 0;
    private WeaponChanger _weaponChanger;

    private void Awake()
    {
        _sceneLoadHandler = GetComponent<SceneLoadHandler>();
        _weaponChanger = GetComponentInChildren<WeaponChanger>();
    }

    private void OnEnable()
    {
        _playerChecker.ConditionIsDone += LevelComplete;
    }

    private void OnDisable()
    {
        _playerChecker.ConditionIsDone -= LevelComplete;
    }

    private void LevelComplete()
    {
        _currentLevel++;
        LoadLevel(_currentLevel);
    }

    public void LoadLevel(int levelNumber)
    {
        switch(levelNumber)
        {
            case 0:
                Tutorial.Load(_sceneLoadHandler);
                break;
            case 1:
                Level1.Load(_sceneLoadHandler);
                break;
        }
    }
}
