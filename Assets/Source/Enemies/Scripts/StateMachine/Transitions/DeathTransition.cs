using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTransition : Transition
{
    private int _zero;

    private void Start()
    {
        _zero = GetComponent<Enemy>().Zero;
    }

    private void Update()
    {
        if (GetComponent<Enemy>().Health<= _zero)
        {
            NeedTransit = true;
        }
    }
}
