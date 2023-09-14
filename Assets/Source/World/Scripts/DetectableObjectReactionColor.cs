using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DetectableObject))]
public class DetectableObjectReactionColor : MonoBehaviour
{
    [SerializeField] private Color _colorReaction = Color.white;
    [SerializeField] private Image _image;

    private IDetectableObject _detectableObject;
    private Color _defaultColor;

    private void Awake()
    {
        _detectableObject= GetComponent<IDetectableObject>();
        _defaultColor = _image.color;
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent += OnGameObjectDetectionReleased;
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
        _detectableObject.OnGameObjectDetectionReleasedEvent -= OnGameObjectDetectionReleased;
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        SetColor(_colorReaction);
    }

    private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
    {
        SetColor(_defaultColor);
    }

    private void SetColor(Color color)
    {
        _image.color = color;
    }
}
