using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float fadeTime = 1f;
    [SerializeField] string ussingPurpose;

    private Image blackBack;
    
    private void Awake()
    {
        blackBack = GetComponent<Image>();
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(blackBack.DOFade(0, 0));

        gameObject.SetActive(false);
    }

    public void FadeOutDOT()
    {
        Sequence seq = DOTween.Sequence();

        seq.SetAutoKill(false);
        seq.OnRewind(() =>
        {
            blackBack.enabled = true;
        });
        seq.Append(blackBack.DOFade(1.0f, fadeTime));
        seq.OnComplete(() =>
        {
            if (ussingPurpose == "Quit")
                Quit();
            else
                blackBack.enabled = false;
        });
    }

    private void Quit()
    {
        Application.Quit();
    }
}
