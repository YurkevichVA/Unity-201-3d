using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle muteAllToggle;
    [SerializeField] private GameObject content;

    void Start()
    {
        LabirinthState.musicVolume = musicVolumeSlider.value;
        LabirinthState.effectsVolume = effectsVolumeSlider.value;
        LabirinthState.isSoundsMuted = muteAllToggle.isOn;

        if (content.activeInHierarchy)
        {
            ShowMenu();
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(content.activeInHierarchy) HideMenu();
            else ShowMenu();
        }
    }
    public void OnMusicVolumeChanged(float volume)
    {
        LabirinthState.MusicVolumeChanged(volume);
    }
    public void OnEffectsVolumeChanged(float volume)
    {
        LabirinthState.EffectsVolumeChanged(volume);
    }
    public void OnMuteAllChanged(bool value)
    {
        LabirinthState.SoundsMuteChanged(value);
    }
    private void ShowMenu()
    {
        content.SetActive(true);
        Time.timeScale = 0f;
    }
    private void HideMenu()
    {
        content.SetActive(false);
        Time.timeScale = 1f;
    }
}
