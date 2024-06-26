using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.World
{
    [RequireComponent(typeof(DetectableObject))]
    public class DetectableObjectReactionColor : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _idleParticle;
        [SerializeField] private ParticleSystem _verifyingParticle;

        private IDetectableObject _detectableObject;

        private void Awake()
        {
            _detectableObject = GetComponent<IDetectableObject>();
            _idleParticle.gameObject.SetActive(true);
            _verifyingParticle.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _detectableObject.GameObjectDetected += OnGameObjectDetect;
            _detectableObject.GameObjectDetectionReleased += OnGameObjectDetectionReleased;
        }

        private void OnDisable()
        {
            _detectableObject.GameObjectDetected -= OnGameObjectDetect;
            _detectableObject.GameObjectDetectionReleased -= OnGameObjectDetectionReleased;
        }

        private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Inventory inventory))
            {
                ChangeParticle();
            }
        }

        private void OnGameObjectDetectionReleased(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Inventory inventory))
            {
                ChangeParticle();
            }
        }

        private void ChangeParticle()
        {
            _idleParticle.gameObject.SetActive(!_idleParticle.gameObject.activeSelf);
            _verifyingParticle.gameObject.SetActive(!_verifyingParticle.gameObject.activeSelf);
        }
    }
}