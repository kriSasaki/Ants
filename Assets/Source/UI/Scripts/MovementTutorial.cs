using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField] private InterfaceAnimator _interfaceAnimator;
    [SerializeField] private GameObject _computerTutorial;
    [SerializeField] private GameObject _mobileTutorial;

    private void OnEnable()
    {
        _interfaceAnimator.OnGameStarted += ShowTutorial;
    }

    private void OnDisable()
    {
        _interfaceAnimator.OnGameStarted -= ShowTutorial;
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
