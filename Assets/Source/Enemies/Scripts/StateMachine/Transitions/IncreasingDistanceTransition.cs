using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingDistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private void Start()
    {
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        Debug.Log(Vector2.Distance(transform.position, Target.transform.position));
        if (Vector2.Distance(transform.position, Target.transform.position) > _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
