using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    private string _deathType;

    private void OnEnable()
    {
        PlayDeath();
    }

    private void PlayDeath()
    {
        _deathType = ((DeathType)Random.Range((int)DeathType.AntDeath0, (int)DeathType.AntDeath02 + 1)).ToString();
        Animator.SetTrigger(_deathType);
    }

    private enum DeathType
    {
        AntDeath0,
        AntDeath01,
        AntDeath02
    }
}
