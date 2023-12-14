using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle muteAllToggle;
    [SerializeField] private GameObject content;

    [SerializeField] private TMP_Dropdown dropdown;
    void Start()
    {
        string[] names = QualitySettings.names;
        if (names.Length != dropdown.options.Count)
        {
            dropdown.options.Clear();
            for (int i = 0; i < names.Length; i++)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(names[i]));
            }
            dropdown.value = QualitySettings.GetQualityLevel();
        }
        else
        {
            OnQualityDropdownChanged(dropdown.value);
        }


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
    public void OnQualityDropdownChanged(int value)
    {
        Debug.Log(value);
        QualitySettings.SetQualityLevel(value, true);
    }
    public void OnMenuButtonClick(int button)
    {
        switch (button)
        {
            case 1: // Exit
                if (Application.isEditor)
                {
                    EditorApplication.ExitPlaymode();
                }
                else
                {
                    Application.Quit(0);
                }
                break;
            case 2:
                musicVolumeSlider.value = 0.5f;
                effectsVolumeSlider.value = 0.5f;
                muteAllToggle.isOn = false;
                OnMusicVolumeChanged(0.5f);
                OnEffectsVolumeChanged(0.5f);
                OnMuteAllChanged(false);
                break;
            case 3:
                HideMenu();
                break;
        }
    }
}
