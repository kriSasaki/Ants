using System.Collections;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Resources
{
    public abstract class Resource : MonoBehaviour
    {
        private const int Amount = 1;

        [SerializeField] private float _pickUpDuration;
        [SerializeField] private PickUpAnimation _pickUpAnimation;
        [SerializeField] private DropAnimation _dropAnimation;

        private bool _resourceCollected = false;
        private Inventory _target;
        private float _time = 0.1f;

        public bool IsPickUpInProgress => _time < _pickUpDuration;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Inventory target) && _resourceCollected == false)
            {
                _resourceCollected = true;
                _target = target;
                Drop();
            }
        }

        public abstract IEnumerator Give(Inventory inventory, int amount);

        public void HandleTick()
        {
            _time += Time.deltaTime;
        }

        private void Drop()
        {
            _pickUpAnimation.SetTargetPosition(_target.transform);
            StartCoroutine(Give(_target, Amount));
            _dropAnimation.enabled = true;
        }
    }
}