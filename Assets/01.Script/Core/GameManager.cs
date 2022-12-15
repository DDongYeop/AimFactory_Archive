using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();

        Load();
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.P))
            PlayerPrefs.DeleteAll();*/

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerX", _player.transform.position.x);
        PlayerPrefs.SetFloat("Playery", _player.transform .position.y);
        PlayerPrefs.Save();
    }

    protected void Load()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("Playery");

        _player.transform.position = new Vector2(x, y);
    }

    protected void ReGame()
    {
        PlayerPrefs.SetFloat("PlayerX", -55);
        PlayerPrefs.SetFloat("Playery", -2);
        PlayerPrefs.Save();
    }
}
