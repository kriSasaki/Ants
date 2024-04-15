using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _bought;
        [SerializeField] private ScaleChanger _scaleChanger;
        [SerializeField] private RankStars _rankStars;
        [SerializeField] private GameObject _buttonAlert;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private ScaleChanger _rightButtonScaleChanger;

        public TMP_Text Price => _price;
        public TMP_Text Bought => _bought;
        public RankStars RankStars => _rankStars;
        public Button BuyButton => _buyButton;

        public void ChangeButtonAlertStatus(bool isNewItemAvailable)
        {
            if (isNewItemAvailable)
            {
                _rightButtonScaleChanger.StartTween();
                _buttonAlert.SetActive(true);
            }
            else
            {
                _rightButtonScaleChanger.StopTween();
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
    }
}
