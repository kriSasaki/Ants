using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class ChaseState : State
{
    private const string Walking = "Walking";

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Animator.SetBool(Walking, true);
    }

    private void OnDisable()
    {
        Animator.SetBool(Walking, false);
    }

    private void Update()
    {
        _agent.SetDestination(Target.transform.position);
    }
}
