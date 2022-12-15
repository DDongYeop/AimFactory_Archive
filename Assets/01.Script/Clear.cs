using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clear : MonoBehaviour
{
    [SerializeField] private RectTransform _clearUI;
    [SerializeField] private GameObject _clearTile;
    [SerializeField] private float moveTime = 1f;

    public bool isClearUIOn = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ClearUIMove(true, 1080);
    }

    public void EndButtonDown()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(GetComponent<RectTransform>().DOScale(0.85f, 0.15f).SetLoops(2, LoopType.Yoyo));
        seq.OnComplete(() =>
        {
            ClearUIMove(false, -1080);
            _clearTile.SetActive(false);
            Invoke("ClearTileReset", 5f);
        });
    }

    private void ClearUIMove(bool isUI, int index)
    {
        isClearUIOn = isUI;

        Sequence seq = DOTween.Sequence();
        seq.Append(_clearUI.DOAnchorPosY(_clearUI.anchoredPosition.y + index, moveTime));
    }

    private void ClearTileReset()
    {
        _clearTile.SetActive(true);
    }
}
