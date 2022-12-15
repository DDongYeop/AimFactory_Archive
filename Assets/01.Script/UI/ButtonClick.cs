using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonClick : GameManager
{
    [SerializeField] int gameSceneNum = 1;
    [SerializeField] string buttonName;

    [SerializeField] GameObject blackBack;

    private FadeOut _fadeout;
    private TitleUI _titleUI;
    private PauseMenu _pauseMenu;
    private GameAudio _gameAudio;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _fadeout = GameObject.Find("BlackBack").GetComponent<FadeOut>();
            _titleUI = GameObject.Find("GameManager").GetComponent<TitleUI>();   
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _pauseMenu = GameObject.Find("GameManager").GetComponent<PauseMenu>();
            _gameAudio = GameObject.Find("GameManager").GetComponent<GameAudio>();
        }
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); //플레이어 프립스의 모든 값을 초기화. 

        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;

        if (!PlayerPrefs.HasKey("PlayerX") && buttonName == "Continue")
            gameObject.SetActive(false);
    }

    public void ButtonClickDOT()
    {
        Time.timeScale = 1;

        Sequence seq = DOTween.Sequence();

        seq.Append(GetComponent<RectTransform>().DOScale(0.85f, 0.15f).SetLoops(2, LoopType.Yoyo));
        seq.AppendCallback(SceneChanger);
    }

    private void SceneChanger()
    {
        switch (buttonName)
        {
            case "Continue":
                print("Continue button down");
                SceneManager.LoadScene(gameSceneNum);
                break;
            case "NewGame":
                print("NewGame button down");
                ReGame();
                SceneManager.LoadScene(gameSceneNum);
                break;
            case "Settings":
                print("Settings button down");
                _titleUI.UIRight();
                break;
            case "Credits":
                print("Credits button down");
                _titleUI.UILeft();
                break;
            case "Quit":
                print("Quit button down");
                blackBack.SetActive(true);
                _fadeout.FadeOutDOT();
                break;
            case "Help":
                print("Help button down");
                _titleUI.UIUp();
                break;
            case "Title":
                print("Title button down");
                _titleUI.UIDown();
                break;
            case "Contineue":
                _pauseMenu.ContinueClick();
                break;
            case "MainMenu":
                _pauseMenu.MainMeueClick();
                break;
            case "MusicChange":
                _gameAudio.SoundChange();
                break;
            case "Right":
                print("Right button down");
                _titleUI.UIRight();
                break;
            case "Left":
                print("Left button down");
                _titleUI.UILeft();
                break;
            default:
                Debug.LogError("Buttonname is null");
                break;
        }
    }
}
