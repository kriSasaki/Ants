using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    private string _deathType;
    private BoxCollider _collider;

    private void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
        PlayDeath();
    }

    private void PlayDeath()
    {
        _collider.enabled = false;
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
