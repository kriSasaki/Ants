using System;
using UnityEngine;

namespace Source.Scripts.World
{
    public class DetectableObject : MonoBehaviour, IDetectableObject
    {
        public event Action<GameObject, GameObject> GameObjectDetected;
        public event Action<GameObject, GameObject> GameObjectDetectionReleased;

        public GameObject GameObject { get; }

        public void Detect(GameObject detectionSource)
        {
            GameObjectDetected?.Invoke(detectionSource, GameObject);
        }

        public void ReleaseDetection(GameObject detectionSource)
        {
            GameObjectDetectionReleased?.Invoke(detectionSource, GameObject);
        }
    }
}