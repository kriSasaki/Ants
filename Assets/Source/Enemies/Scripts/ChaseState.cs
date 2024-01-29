using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private const string Walking = "Walking";

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
