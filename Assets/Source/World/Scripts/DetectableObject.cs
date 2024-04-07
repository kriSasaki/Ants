using System;
using UnityEngine;

namespace Source.World.Scripts
{
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event Action<GameObject, GameObject> GameObjectDetected;
        public event Action<GameObject, GameObject> GameObjectDetectionReleased;

        public void Detected(GameObject detectionSource)
        {
            GameObjectDetected?.Invoke(detectionSource, gameObject);
        }

        public void DetectionReleased(GameObject detectionSource)
        {
            GameObjectDetectionReleased?.Invoke(detectionSource, gameObject);
        }
    }
}
