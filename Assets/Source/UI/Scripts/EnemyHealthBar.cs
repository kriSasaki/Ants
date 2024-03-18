using UnityEngine;

public class EnemyHealthBar : Bar
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.OnHealthChange += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable()
    {
        _enemy.OnHealthChange -= OnValueChanged;
    }
}
