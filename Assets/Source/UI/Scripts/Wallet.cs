using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldDisplay;

    public int GoldAmount => _goldAmount;

    private int _goldAmount =100;
    private Ad _ad;

    private void Awake()
    {
        _ad = GetComponentInParent<Ad>();
        _goldDisplay.text = _goldAmount.ToString();
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
        _goldDisplay.text = _goldAmount.ToString();
    }
}
