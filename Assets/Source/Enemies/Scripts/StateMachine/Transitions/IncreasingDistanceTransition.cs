using UnityEngine;

namespace Source.Enemies.Scripts.StateMachine.Transitions
{
    public class IncreasingDistanceTransition : Transition
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _transitionRange;
        [SerializeField] private float _rangeSpread;

        private void Start()
        {
            _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > _transitionRange)
            {
                NeedTransit = true;
            }
        }
    }
}
