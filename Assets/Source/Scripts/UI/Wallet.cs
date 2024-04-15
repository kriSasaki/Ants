using System;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Wallet : MonoBehaviour
    {
        private const string GoldAmountKey = "GoldAmount";

        public event Action GoldAmountChanged;
        public event Action<string, Action<int>> LoadDataNeeded;
        public event Action<string, int> SaveDataNeeded;
        public int GoldAmount { get; private set; }

        private AdShower _adShower;

        private void Awake()
        {
            _adShower = GetComponentInParent<AdShower>();
        }

        private void Start()
        {
            LoadDataNeeded?.Invoke(GoldAmountKey, ChangeGoldAmount);
        }

        private void OnEnable()
        {
            _adShower.Rewarded += ChangeGoldAmount;
        }

        private void OnDisable()
        {
            _adShower.Rewarded -= ChangeGoldAmount;
        }

        public void ChangeGoldAmount(int amount)
        {
            GoldAmount += amount;
            GoldAmountChanged?.Invoke();
            SaveDataNeeded?.Invoke(GoldAmountKey, GoldAmount);
        }
    }
}
