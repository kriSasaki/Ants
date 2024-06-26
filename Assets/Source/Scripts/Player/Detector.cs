using System.Collections.Generic;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Detector : MonoBehaviour
    {
        private readonly List<GameObject> _detectedObjects = new();

        private void Detect(IDetectableObject detectableObject)
        {
            if (!_detectedObjects.Contains(detectableObject.GameObject))
            {
                detectableObject.Detect(gameObject);
                _detectedObjects.Add(detectableObject.GameObject);
            }
        }

        private void ReleaseDetection(IDetectableObject detectableObject)
        {
            if (_detectedObjects.Contains(detectableObject.GameObject))
            {
                detectableObject.ReleaseDetection(gameObject);
                _detectedObjects.Remove(detectableObject.GameObject);
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