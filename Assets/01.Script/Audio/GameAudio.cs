using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _gameAudioClip;

    private AudioSource _audioSource;
    private int _currentNum;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = _gameAudioClip[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            SoundChange();
    }

    public void SoundChange()
    {
        _currentNum++;

        if (_currentNum == _gameAudioClip.Length)
        {
            _currentNum = 0;
            _audioSource.clip = _gameAudioClip[_currentNum];
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = _gameAudioClip[_currentNum];
            _audioSource.Play();
        }
    }
}
