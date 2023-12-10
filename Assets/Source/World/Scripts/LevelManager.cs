using UnityEngine;
using IJunior.TypedScenes;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    public int OpenedLevels { get; private set; }
    public int CurrentLevel => _currentLevel;

    private SceneLoadHandler _sceneLoadHandler;
    private int _currentLevel = 0;

    private void Awake()
    {
        _sceneLoadHandler = GetComponent<SceneLoadHandler>();
    }

    private void OnEnable()
    {
        _playerChecker.ConditionIsDone += LevelComplete;
    }

    private void OnDisable()
    {
        _playerChecker.ConditionIsDone -= LevelComplete;
    }

    public void SaveLevels(int currentLevel, int openedLevels)
    {
        _currentLevel = currentLevel;
        OpenedLevels = openedLevels;
    }

    public void LoadLevel(int levelNumber)
    {
        _currentLevel = levelNumber;

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

    private void LevelComplete()
    {
        if(OpenedLevels == _currentLevel)
        {
            OpenedLevels++;
        }

        _currentLevel++;
        LoadLevel(_currentLevel);
    }
}
