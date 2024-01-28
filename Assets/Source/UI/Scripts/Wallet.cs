using System;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private const string GoldAmountKey = "GoldAmount";

    [SerializeField] private TMP_Text _goldDisplay;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public int GoldAmount => _goldAmount;

    private int _goldAmount;
    private Ad _ad;

    private void Awake()
    {
        _ad = GetComponentInParent<Ad>();
        _goldDisplay.text = _goldAmount.ToString();
    }

    private void Start()
    {
        OnLoadDataNeeded?.Invoke(GoldAmountKey, data =>
        {
            ChangeGoldAmount(data);
        });
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
        _goldAmount += amount;
        UpdateGold();
    }

    private void UpdateGold()
    {
        OnSaveDataNeeded?.Invoke(GoldAmountKey, _goldAmount);
        _goldDisplay.text = _goldAmount.ToString();
    }
}
