using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private InterfaceManager _interfaceManager;

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
        GetComponentInChildren<TMP_Text>().enabled = true;
    }
}
