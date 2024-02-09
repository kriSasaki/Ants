using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementTutorial : MonoBehaviour
{
    [FormerlySerializedAs("_interfaceAnimator")] [SerializeField] private InterfaceVisualizer interfaceVisualizer;
    [SerializeField] private GameObject _computerTutorial;
    [SerializeField] private GameObject _mobileTutorial;

    private void OnEnable()
    {
        interfaceVisualizer.OnGameStarted += ShowTutorial;
    }

    private void OnDisable()
    {
        interfaceVisualizer.OnGameStarted -= ShowTutorial;
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
