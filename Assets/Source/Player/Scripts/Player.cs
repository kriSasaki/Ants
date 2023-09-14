using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event Action<int, int> OnHealthChange;

    private int _maxHealth;
    private int _minHealth=0;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void GetDamage(int damage)
    {
        _health-=damage;
        OnHealthChange?.Invoke(_health, _maxHealth);

        if(_health <= _minHealth)
        {
            //
        }
    }
}
