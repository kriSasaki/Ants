using System;
using System.Collections.Generic;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Detector : MonoBehaviour, IDetector
    {
        public event Action<GameObject, GameObject> GameObjectDetected;
        public event Action<GameObject, GameObject> GameObjectDetectionReleased;

        private List<GameObject> _detectedObjects = new List<GameObject>();

        public void Detect(IDetectableObject detectableObject)
        {
            if (!_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.Detect(gameObject);
                _detectedObjects.Add(detectableObject.gameObject);

                GameObjectDetected?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

        public void Detect(GameObject detectedObject)
        {
            if (!_detectedObjects.Contains(detectedObject))
            {
                _detectedObjects.Add(detectedObject);

                GameObjectDetected?.Invoke(gameObject, detectedObject);
            }
        }

        public void ReleaseDetection(IDetectableObject detectableObject)
        {
            if (_detectedObjects.Contains(detectableObject.gameObject))
            {
                detectableObject.DetectionRelease(gameObject);
                _detectedObjects.Remove(detectableObject.gameObject);

                GameObjectDetectionReleased?.Invoke(gameObject, detectableObject.gameObject);
            }
        }

        public void ReleaseDetection(GameObject detectedObject)
        {
            if (_detectedObjects.Contains(detectedObject))
            {
                _detectedObjects.Remove(detectedObject);

                GameObjectDetectionReleased?.Invoke(gameObject, detectedObject);
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
}
