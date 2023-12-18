using UnityEngine;
using IJunior.TypedScenes;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    public int OpenedLevels { get; private set; }
    public int CurrentLevel => _currentLevel;

    private SceneLoadHandler _sceneLoadHandler;
    private RewardWindow _rewardWindow;
    private int _currentLevel;

    private void Awake()
    {
        _rewardWindow = GetComponentInChildren<RewardWindow>();
        _sceneLoadHandler = GetComponent<SceneLoadHandler>();
        _rewardWindow.SetCurrentLevel(CurrentLevel);
    }

    private void OnEnable()
    {
        _rewardWindow.OnLevelComplete += LevelComplete;
        _rewardWindow.OnButtonPressed += LoadNextLevel;
    }

    private void OnDisable()
    {
        _rewardWindow.OnLevelComplete -= LevelComplete;
        _rewardWindow.OnButtonPressed -= LoadNextLevel;
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
            case 2:
                Level2.Load(_sceneLoadHandler);
                break;
        }
    }

    private void LevelComplete(bool isLost)
    {
        if (!isLost && OpenedLevels == _currentLevel)
        {
            OpenedLevels++;
        }
    }

    private void LoadNextLevel()
    {
        _currentLevel++;
        LoadLevel(_currentLevel);
    }
}
