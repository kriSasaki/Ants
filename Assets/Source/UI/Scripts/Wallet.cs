using System;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private const string GoldAmountKey = "GoldAmount";

    public event Action GoldAmountChanged;
    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public int GoldAmount { get; private set; } = 150;

    private Ad _ad;

    private void Awake()
    {
        _ad = GetComponentInParent<Ad>();
    }

    private void Start()
    {
        OnLoadDataNeeded?.Invoke(GoldAmountKey, ChangeGoldAmount);
    }

    private void OnEnable()
    {
        _ad.Rewarded += ChangeGoldAmount;
    }

    private void OnDisable()
    {
        _ad.Rewarded -= ChangeGoldAmount;
    }

    public void ChangeGoldAmount(int amount)
    {
        GoldAmount += amount;
        GoldAmountChanged?.Invoke();
        OnSaveDataNeeded?.Invoke(GoldAmountKey, GoldAmount);
    }
}
