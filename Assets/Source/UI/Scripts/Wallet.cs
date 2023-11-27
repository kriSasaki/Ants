using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldDisplay;

    public int GoldAmount => _goldAmount;

    private int _goldAmount=100;

    private void Awake()
    {
        _goldDisplay.text = _goldAmount.ToString();
    }

    private void UpdateGold()
    {
        _goldDisplay.text = _goldAmount.ToString();
    }

    public void ChangeGoldAmount(int amount)
    {
        _goldAmount += amount;
        UpdateGold();
    }
}
