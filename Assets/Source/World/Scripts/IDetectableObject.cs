using System;
using UnityEngine;

namespace Source.World.Scripts
{
    public interface IDetectableObject
    {
        event Action<GameObject, GameObject> GameObjectDetected;
        event Action<GameObject, GameObject> GameObjectDetectionReleased;

        GameObject gameObject { get; }

        void Detected(GameObject detectionSource);
        void DetectionReleased(GameObject detectionSource);
    }
}
