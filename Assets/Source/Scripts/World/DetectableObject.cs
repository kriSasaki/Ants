using System;
using UnityEngine;

namespace Source.Scripts.World
{
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event Action<GameObject, GameObject> GameObjectDetected;
        public event Action<GameObject, GameObject> GameObjectDetectionReleased;

        public void Detect(GameObject detectionSource)
        {
            GameObjectDetected?.Invoke(detectionSource, gameObject);
        }

        public void ReleaseDetection(GameObject detectionSource)
        {
            GameObjectDetectionReleased?.Invoke(detectionSource, gameObject);
        }
    }
}