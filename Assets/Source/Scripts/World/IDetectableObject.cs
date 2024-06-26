using System;
using UnityEngine;

namespace Source.Scripts.World
{
    public interface IDetectableObject
    {
        event Action<GameObject, GameObject> GameObjectDetected;
        event Action<GameObject, GameObject> GameObjectDetectionReleased;

        public GameObject GameObject { get; }

        void Detect(GameObject detectionSource);
        void ReleaseDetection(GameObject detectionSource);
    }
}