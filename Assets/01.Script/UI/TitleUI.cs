using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    [Header ("GameObject")]
    [SerializeField] RectTransform credits;
    [SerializeField] RectTransform main;
    [SerializeField] RectTransform setting;
    [SerializeField] RectTransform help;

    [Header ("Variable")]
    [SerializeField] float moveTime = 1f;

    private bool _isMove = false;


    public void UIRight()
    {
        if (_isMove)
            return;
        MoveRL(-1920);
    }

    public void UILeft()
    {
        if (_isMove)
            return;
        MoveRL(1920);
    }

    private void MoveRL(int index)
    {
        _isMove = true;

        Sequence seq = DOTween.Sequence();

        seq.Append(credits.DOAnchorPosX(credits.anchoredPosition.x + index, moveTime));
        seq.Join(main.DOAnchorPosX(main.anchoredPosition.x + index, moveTime));
        seq.Join(setting.DOAnchorPosX(setting.anchoredPosition.x + index, moveTime));
        seq.Join(help.DOAnchorPosX(help.anchoredPosition.x + index, moveTime));
        seq.OnComplete(() =>
        {
            _isMove = false;
        });
    }

    public void UIUp()
    {
        if (_isMove)
            return;
        MoveUD(1080);
    }

    public void UIDown()
    {
        if (_isMove)
            return;
        MoveUD(-1080);
    }

    private void MoveUD(int index)
    {
        _isMove = true;
        
        Sequence seq = DOTween.Sequence();

        seq.Append(credits.DOAnchorPosY(credits.anchoredPosition.y + index, moveTime));
        seq.Join(main.DOAnchorPosY(main.anchoredPosition.y + index, moveTime));
        seq.Join(setting.DOAnchorPosY(setting.anchoredPosition.y + index, moveTime));
        seq.Join(help.DOAnchorPosY(help.anchoredPosition.y + index, moveTime));
        seq.OnComplete(() =>
        {
            _isMove = false;
        });
    }
}
