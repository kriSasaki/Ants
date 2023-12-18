public class EggsAmount : Amount
{
    private void OnEnable()
    {
        _inventory.EggsAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _inventory.EggsAmountChanged -= OnValueChanged;
    }
}
