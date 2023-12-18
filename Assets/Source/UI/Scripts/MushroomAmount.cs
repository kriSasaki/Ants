public class MushroomAmount : Amount
{
    private void OnEnable()
    {
        _inventory.MushroomsAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _inventory.MushroomsAmountChanged -= OnValueChanged;
    }
}
