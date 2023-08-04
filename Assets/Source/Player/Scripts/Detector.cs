using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Detector : MonoBehaviour, IDetector
{
    public event ObjectDetectedHandler OnGameObjectDetectedEvent;
    public event ObjectDetectedHandler OnGameObjectDetectionReleasedEvent;

    public GameObject[] DetectedObjects => _detectedObjects.ToArray();

    private List<GameObject> _detectedObjects = new List<GameObject>();

    public void Detect(IDetectableObject detectableObject)
    {
        if (!_detectedObjects.Contains(detectableObject.gameObject))
        {
            detectableObject.Detected(gameObject);
            _detectedObjects.Add(detectableObject.gameObject);

            OnGameObjectDetectedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    public void Detect(GameObject detectedObject)
    {
        if (!_detectedObjects.Contains(detectedObject))
        {
            _detectedObjects.Add(detectedObject);

            OnGameObjectDetectedEvent?.Invoke(gameObject, detectedObject);
        }
    }

    public void ReleaseDetection(IDetectableObject detectableObject)
    {
        if (_detectedObjects.Contains(detectableObject.gameObject))
        {
            detectableObject.DetectionReleased(gameObject);
            _detectedObjects.Remove(detectableObject.gameObject);

            OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectableObject.gameObject);
        }
    }

    public void ReleaseDetection(GameObject detectedObject)
    {
        if (_detectedObjects.Contains(detectedObject))
        {
            _detectedObjects.Remove(detectedObject);

            OnGameObjectDetectionReleasedEvent?.Invoke(gameObject, detectedObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedObject))
        {
            Detect(detectedObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsColliderDetectableObject(other, out var detectedObject))
        {
            ReleaseDetection(detectedObject);
        }
    }

    private bool IsColliderDetectableObject(Collider collider, out IDetectableObject detectedObject)
    {
        detectedObject = collider.GetComponentInParent<IDetectableObject>();

        return detectedObject != null;
    }
}
