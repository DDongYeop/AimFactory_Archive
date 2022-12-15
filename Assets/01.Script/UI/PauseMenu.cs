using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] float moveTime = 1f;

    private Clear _clear;

    private RectTransform pauseRet;
    private Transform _player;

    private bool _isToggled = false;
    private bool _isMove = false;

    private void Awake()
    {
        _clear = GameObject.Find("Clear Collider").GetComponent<Clear>();

        _player = GameObject.Find("Player").GetComponent<Transform>();
        pauseRet = GameObject.Find("PausePanel").GetComponent<RectTransform>();
    }

    #region Pausemove
    private void Update()
    {
        if (_isMove || _clear.isClearUIOn)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isToggled)
            {
                Time.timeScale = 1;
                PauseMenuMove(1080);
            }
            else
            {
                PauseMenuMove(-1080);
            }
        }
    }

    private void PauseMenuMove(int movePos)
    {
        _isMove = true;

        Sequence seq = DOTween.Sequence();
        seq.Append(pauseRet.DOAnchorPosY(pauseRet.anchoredPosition.y + movePos, moveTime));
        seq.OnComplete(() =>
        {
            Pause(movePos);
        });
    }

    private void Pause(int movePos)
    {
        switch (movePos)
        {
            case 1080:
                _isToggled = false;
                break;
            case -1080:
                Time.timeScale = 0;
                _isToggled = true;
                break;
        }
        _isMove = false;
    }
    #endregion

    #region Button
    public void ContinueClick()
    {
        if (_isMove)
            return;
        Time.timeScale = 1;
        PauseMenuMove(1080);
    }

    public void MainMeueClick()
    {
        Time.timeScale = 1;

        PlayerPrefs.SetFloat("PlayerX", _player.transform.position.x);
        PlayerPrefs.SetFloat("Playery", _player.transform.position.y);
        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }
    #endregion


}
