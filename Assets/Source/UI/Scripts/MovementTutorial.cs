using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField] private InterfaceManager _interfaceManager;
    [SerializeField] private GameObject _computerTutorial;
    [SerializeField] private GameObject _mobileTutorial;

    private void OnEnable()
    {
        _interfaceManager.OnGameStarted += ShowTutorial;
    }

    private void OnDisable()
    {
        _interfaceManager.OnGameStarted -= ShowTutorial;
    }

    private void ShowTutorial()
    {
        if (Application.isMobilePlatform)
        {
            _mobileTutorial.SetActive(true);
        }
        else
        {
            _computerTutorial.SetActive(true);
        }
    }
}
