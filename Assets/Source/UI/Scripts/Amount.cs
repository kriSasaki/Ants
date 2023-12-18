using TMPro;
using UnityEngine;

public class Amount : MonoBehaviour
{
    protected Inventory _inventory;

    private int _amount;
    private TMP_Text _textAmount;

    private void Awake()
    {
        _inventory = GetComponentInParent<PlayerTransmitter>().Inventory;
    }

    private void Start()
    {
        _textAmount = GetComponent<TMP_Text>();
    }

    public void OnValueChanged(int amount)
    {
        _amount = amount;
        _textAmount.text = _amount.ToString();
    }
}
