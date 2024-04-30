namespace Source.Scripts.UI
{
    public class EggsAmount : Amount
    {
        private void OnEnable()
        {
            Inventory.EggsAmountChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            Inventory.EggsAmountChanged -= OnValueChanged;
        }
    }
}