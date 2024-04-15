using Source.Scripts.Enemies.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Enemies
{
    public class ChaseState : State
    {
        private readonly int Walking = Animator.StringToHash("Walking");

        private NavMeshAgent _agent;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
            Animator.SetBool(Walking, true);
            _agent.enabled = true;
        }

        private void OnDisable()
        {
            Animator.SetBool(Walking, false);
            _agent.enabled = false;
        }

        private void Update()
        {
            _agent.SetDestination(Target.transform.position);
        }
    }
}
