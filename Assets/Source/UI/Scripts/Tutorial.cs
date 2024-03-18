using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    [FormerlySerializedAs("interfaceAnimator")] [SerializeField] private InterfaceVisualizer interfaceVisualizer;

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
        GetComponentInChildren<TMP_Text>().enabled = true;
    }
}
