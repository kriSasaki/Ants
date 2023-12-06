using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour, ISceneLoadHandler<SceneLoadHandler>
{
    [SerializeField] private TMP_Text _goldDisplay;

    public int GoldAmount => _goldAmount;

    private int _goldAmount=100;

    private void Awake()
    {
        _goldDisplay.text = _goldAmount.ToString();
    }

    public void OnSceneLoaded(SceneLoadHandler argument)
    {
        
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
