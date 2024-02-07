using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private InterfaceAnimator interfaceAnimator;

    private void OnEnable()
    {
        interfaceAnimator.OnGameStarted += ShowTutorial;
    }

    private void OnDisable()
    {
        interfaceAnimator.OnGameStarted -= ShowTutorial;
    }

    private void ShowTutorial()
    {
        GetComponentInChildren<TMP_Text>().enabled = true;
    }
}
