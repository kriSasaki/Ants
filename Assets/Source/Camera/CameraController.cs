using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InterfaceVisualizer _interfaceVisualizer;

    private CameraFollower _cameraFollower;

    private void Awake()
    {
        _cameraFollower = GetComponent<CameraFollower>();
    }

    private void OnEnable()
    {
        _interfaceVisualizer.OnGameStarted += Enable;
    }

    private void OnDisable()
    {
        _interfaceVisualizer.OnGameStarted -= Enable;
    }
    
    private void Enable()
    {
        _cameraFollower.enabled = true;
    }
}
