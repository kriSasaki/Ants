using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] protected TMP_Text _price;
    [SerializeField] protected TMP_Text _buyed;
    [SerializeField] protected ScaleChanger _scaleChanger;
    [SerializeField] protected GameObject _buttonAlert;
    [SerializeField] protected Button _buyButton;
    [SerializeField] protected Button _leftButton;
    [SerializeField] protected Button _rightButton;

    public void ChangeButtonAlertStatus(bool isNewItemAvailable)
    {
        if (isNewItemAvailable)
        {
            _rightButton.GetComponent<ScaleChanger>().StartTween();
            _buttonAlert.SetActive(true);
        }
        else
        {
            _rightButton.GetComponent<ScaleChanger>().StopTween();
            _buttonAlert.SetActive(false);
        }
    }

    public void ChangePriceAlertStatus(bool isNewItemAvailable)
    {
        if (isNewItemAvailable)
        {
            _scaleChanger.StartTween();
        }
        else
        {
            _scaleChanger.StopTween();
        }
    }

    public void ChangeInteractivity(bool isEnable)
    {
        _buyButton.enabled = isEnable;
        _leftButton.enabled = isEnable;
        _rightButton.enabled = isEnable;
    }
}
