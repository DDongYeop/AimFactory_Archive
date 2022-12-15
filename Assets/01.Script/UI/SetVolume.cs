using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private Slider _matserVolumeSlider;
    private Slider _bgmVolumeSlider;
    private Slider _effectVolumeSlider;

    private void Awake()
    {
        if (SceneManager.sceneCount == 0 || SceneManager.sceneCount == 1)
        {
            _matserVolumeSlider = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
            _bgmVolumeSlider = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>();
            _effectVolumeSlider = GameObject.Find("EffectVolumeSlider").GetComponent<Slider>();
        }

    }

    private void Start()
    {
        Test();
        
        _matserVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        _effectVolumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    }

    private void Test()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
            PlayerPrefs.SetFloat("MasterVolume", 0.75f);
        if (!PlayerPrefs.HasKey("BGMVolume"))
            PlayerPrefs.SetFloat("BGMVolume", 0.75f);
        if (!PlayerPrefs.HasKey("EffectVolume"))
            PlayerPrefs.SetFloat("EffectVolume", 0.75f);
    }

    #region Slider
    public void MasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void BGMVolume(float sliderValue)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
    }

    public void EffectVolume(float sliderValue)
    {
        audioMixer.SetFloat("EffectVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
    }
    #endregion
}
