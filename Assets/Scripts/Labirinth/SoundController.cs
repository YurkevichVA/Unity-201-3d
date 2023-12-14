using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer soundMixer;

    void Start()
    {
        LabirinthState.OnSoundsMuteChanged += SoundsMuteChanged;
        LabirinthState.OnMusicVolumeChanged += MusicVolumeChanged;
        LabirinthState.OnEffectsVolumeChanged += EffectsVolumeChanged;

        MusicVolumeChanged();
        EffectsVolumeChanged();
    }
    
    public void SoundsMuteChanged()
    {
        if (LabirinthState.isSoundsMuted)
        {
            soundMixer.SetFloat("MusicVolume", -80f);
            soundMixer.SetFloat("EffectsVolume", -80f);
        }
        else
        {
            MusicVolumeChanged();
            EffectsVolumeChanged();
        }
    }

    public void MusicVolumeChanged()
    {
        if (!LabirinthState.isSoundsMuted)
        {
            float dB = -80f + 90f * LabirinthState.musicVolume;
            soundMixer.SetFloat("MusicVolume", dB);
        }
    }

    public void EffectsVolumeChanged()
    {
        if (!LabirinthState.isSoundsMuted)
        {
            float dB = -80 + 90f * LabirinthState.effectsVolume;
            soundMixer.SetFloat("EffectsVolume", dB);
        }
    }

    private void OnDestroy()
    {
        LabirinthState.OnSoundsMuteChanged -= SoundsMuteChanged;
        LabirinthState.OnMusicVolumeChanged -= MusicVolumeChanged;
        LabirinthState.OnEffectsVolumeChanged -= EffectsVolumeChanged;
    }
}
