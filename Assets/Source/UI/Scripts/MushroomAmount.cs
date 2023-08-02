using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAmount : Amount
{
    private void OnEnable()
    {
        _player.MushroomsAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.MushroomsAmountChanged -= OnValueChanged;
    }
}
