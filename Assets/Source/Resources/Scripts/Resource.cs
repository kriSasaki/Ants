using System.Collections;
using Source.Player.Scripts;
using UnityEngine;

namespace Source.Resources.Scripts
{
    public abstract class Resource : MonoBehaviour
    {
        private const int _amount = 1;

        [SerializeField] private float _pickUpDuration;
        [SerializeField] private PickUpAnimation _pickUpAnimation;
        [SerializeField] private DropAnimation _dropAnimation;

        public bool IsPickUpInProgress => _time < _pickUpDuration;
    
        private bool ResourceCollected = false;
        private Player.Scripts.Player _target;
        private float _time = 0.1f;
    
        public abstract IEnumerator GiveResource(Inventory inventory, int amount);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Scripts.Player target) && ResourceCollected == false)
            {
                ResourceCollected = true;
                _target = target;
                Drop();
            }
        }

        public void HandleTick()
        {
            _time += Time.deltaTime;
        }
    
        private void Drop()
        {
            _pickUpAnimation.SetTargetPosition(_target.transform);
            StartCoroutine(GiveResource(_target.GetComponent<Inventory>(), _amount));
            _dropAnimation.enabled = true;
        }
    }
}