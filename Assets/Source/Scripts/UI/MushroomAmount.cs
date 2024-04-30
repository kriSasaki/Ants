namespace Source.Scripts.UI
{
    public class MushroomAmount : Amount
    {
        private void OnEnable()
        {
            Inventory.MushroomsAmountChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            Inventory.MushroomsAmountChanged -= OnValueChanged;
        }
    }
}