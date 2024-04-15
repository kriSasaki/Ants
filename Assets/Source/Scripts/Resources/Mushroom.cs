using System.Collections;
using Source.Scripts.Player;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Resources
{
    public class Mushroom : MonoBehaviour
    {
        private const int Amount = 1;

        [SerializeField] private float _jumpDuration;
        [SerializeField] private PickUpAnimation _pickUpAnimation;
        [SerializeField] private SphereCollider _sphereCollider;

        private IDetectableObject _detectableObject;
        private float _time = 0.5f;

        private void Awake()
        {
            _detectableObject = GetComponent<IDetectableObject>();
        }

        private void OnEnable()
        {
            _detectableObject.GameObjectDetected += OnGameObjectDetect;
        }

        private void OnDisable()
        {
            _detectableObject.GameObjectDetected -= OnGameObjectDetect;
        }

        private void JumpIn(Transform playerPosition)
        {
            _sphereCollider.enabled = false;
            _pickUpAnimation.SetTargetPosition(playerPosition);
            _pickUpAnimation.enabled = true;
            Destroy(gameObject, _jumpDuration);
        }

        private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Inventory inventory))
            {
                JumpIn(inventory.transform);
                StartCoroutine(GiveResource(inventory));
            }
        }

        private IEnumerator GiveResource(Inventory inventory)
        {
            while (_time < _jumpDuration)
            {
                _time += Time.deltaTime;
                yield return null;
            }

            inventory.ChangeMushroomsAmount(Amount);
        }
    }
}