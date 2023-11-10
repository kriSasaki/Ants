using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAnts : MonoBehaviour
{
    private const string AntHurt = "AntHurt";

    private Enemy _enemy;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.OnHealthChange += GetHurt;
    }

    private void OnDisable()
    {
        _enemy.OnHealthChange -= GetHurt;
    }

    private void GetHurt(int health, int maxHealth)
    {
        _animator.SetTrigger(AntHurt);
    }
}
