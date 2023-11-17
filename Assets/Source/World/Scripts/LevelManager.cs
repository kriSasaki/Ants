using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using IJunior.TypedScenes;

public class LevelManager : MonoBehaviour
{
    private int _currentLevel = 0;

    public void AddLevel()
    {
        _currentLevel++;
    }

    public void LoadLevel(int levelNumber)
    {
        switch(levelNumber)
        {
            case 1:
                Level1.Load();
                break;

        }
    }
}
