using Source.Player.Scripts;
using TMPro;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class Amount : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private TMP_Text _textAmount;

        public Inventory Inventory => _inventory;
        
        private int _amount;
        
        public void OnValueChanged(int amount)
        {
            _amount = amount;
            _textAmount.text = _amount.ToString();
        }
    }
}
