using DG.Tweening;
using System;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private Tweener _tween;
    private readonly Vector3 _initialScale = Vector3.one;

    private void OnDisable()
    {
        _rectTransform.DOKill();
    }

    public void StartTween()
    {
        _rectTransform.localScale = _initialScale;
        _tween?.Kill();
        _tween = _rectTransform.DOScale(1.1f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public void StopTween()
    {
        _rectTransform.localScale = _initialScale;
        _tween.Kill();
    }
}