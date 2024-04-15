using System;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Player
{
    public interface IDetector
    {
        event Action<GameObject, GameObject> GameObjectDetected;
        event Action<GameObject, GameObject> GameObjectDetectionReleased;

        void Detect(IDetectableObject detectableObject);
        void Detect(GameObject detectedObject);
        void ReleaseDetection(IDetectableObject detectableObject);
        void ReleaseDetection(GameObject detectedObject);
    }
}
