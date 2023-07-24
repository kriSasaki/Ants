using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceOfChunk : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void GetUp()
    {
        transform.DOMoveY(-1.1019004f, _speed).SetEase(Ease.OutBack);
    }
}
