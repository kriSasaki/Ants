using System;
using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _goldDisplay;

    private void OnEnable()
    {
        _wallet.GoldAmountChanged += ChangeCoinView;
    }

    private void OnDisable()
    {
        _wallet.GoldAmountChanged -= ChangeCoinView;
    }

    private void ChangeCoinView()
    {
        _goldDisplay.text = _wallet.GoldAmount.ToString();
    }
}
