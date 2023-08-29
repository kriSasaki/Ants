using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Amount : MonoBehaviour
{
    [SerializeField] protected Inventory _inventory;

    private int _amount;
    private TMP_Text _textAmount;

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
