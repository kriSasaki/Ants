using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ScaleChanger : MonoBehaviour
    {
        private const float MaxScale = 1.1f;
        private const float Duration = 0.5f;
        private const int InfiniteLoops = -1;
        private readonly Vector3 _initialScale = Vector3.one;

        [SerializeField] private RectTransform _rectTransform;

        private Tweener _tween;

        private void OnDisable()
        {
            _rectTransform.DOKill();
        }

        public void StartTween()
        {
            _rectTransform.localScale = _initialScale;
            _tween?.Kill();
            _tween = _rectTransform.DOScale(
                MaxScale, Duration).SetEase(Ease.InOutSine).SetLoops(
                InfiniteLoops, LoopType.Yoyo).SetUpdate(true);
        }

        public void StopTween()
        {
            _rectTransform.localScale = _initialScale;
            _tween.Kill();
        }
    }
}