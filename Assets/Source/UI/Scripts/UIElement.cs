using DG.Tweening;
using UnityEngine;

namespace Source.UI.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour
    {
        private const int EnableScale = 1;
        private const int DisableScale = 0;
        private const float _changeDuration = 0.4f;

        [SerializeField] private CanvasGroup _canvasGroup;
    

        public void Show()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            gameObject.transform.DOScale(EnableScale, _changeDuration).SetEase(Ease.InBack).SetUpdate(true);
        }

        public void Hide()
        {
            gameObject.transform.DOScale(DisableScale, _changeDuration).SetEase(Ease.InBack).SetUpdate(true)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                });
        }
    }
}