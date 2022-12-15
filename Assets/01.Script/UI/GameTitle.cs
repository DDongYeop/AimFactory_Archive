using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameTitle : MonoBehaviour
{
    private void Start()
    {
        GameNameScale();
    }

    private void GameNameScale()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(525, 1f).SetLoops(2, LoopType.Yoyo));
        seq.AppendCallback(GameNameScale);
    }
}
