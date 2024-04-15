using System;
using UnityEngine;

namespace Source.Scripts.World
{
    public interface IDetectableObject
    {
        event Action<GameObject, GameObject> GameObjectDetected;
        event Action<GameObject, GameObject> GameObjectDetectionReleased;

        GameObject gameObject { get; }

        void Detect(GameObject detectionSource);
        void DetectionRelease(GameObject detectionSource);
    }
}
