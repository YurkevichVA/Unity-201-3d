using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LabirinthState
{
    private static float _checkPoint1Amount;
    public static float checkPoint1Amount
    {
        get { return _checkPoint1Amount; }
        set
        {
            if (_checkPoint1Amount != value)
            {
                _checkPoint1Amount = value;
                NotifyListeners();
            }
        }
    }
    private static float _checkPoint2Amount;
    public static float checkPoint2Amount
    {
        get { return _checkPoint2Amount; }
        set
        {
            if (_checkPoint2Amount != value)
            {
                _checkPoint2Amount = value;
                NotifyListeners();
            }
        }
    }

    public static bool _isCheckPoint2ActivatorPassed = false;
    public static bool isCheckPoint2ActivatorPassed
    {
        get { return _isCheckPoint2ActivatorPassed; }
        set
        {
            if (_isCheckPoint2ActivatorPassed != value)
            {
                _isCheckPoint2ActivatorPassed = value;
                NotifyListeners();
            }
        }
    }

    public static bool _checkPoint1Passed;
    public static bool checkPoint1Passed
    {
        get { return _checkPoint1Passed; }
        set
        {
            if (_checkPoint1Passed != value)
            {
                _checkPoint1Passed = value;
                NotifyListeners();
            }
        }
    }
    public static bool _checkPoint2Passed;
    public static bool checkPoint2Passed
    {
        get { return _checkPoint2Passed; }
        set
        {
            if (_checkPoint2Passed != value)
            {
                _checkPoint2Passed = value;
                NotifyListeners();
            }
        }
    }
    public static bool cameraFirstPerson;
    public static bool isDay;

    public static float musicVolume = 0.5f;
    public static float effectsVolume = 0.5f;
    public static bool isSoundsMuted;

    private static Dictionary<String, List<Action>> propertyObservers = initPropertyObservers();
    private static Dictionary<String, List<Action>> initPropertyObservers()
    {
        Dictionary<String, List<Action>> res = new();
        foreach(var prop in typeof(LabirinthState).GetProperties())
        {
            res[prop.Name] = new();
        }
        return res;
    }
    public static void AddPropertyListener(String properyName, Action listener)
    {
        if (propertyObservers.ContainsKey(properyName))
            propertyObservers[properyName].Add(listener);
        else
            throw new ArgumentException($"'{properyName}' Could not be observed");
    }
    public static void RemovePropertyListener(String properyName, Action listener)
    {
        if (propertyObservers.ContainsKey(properyName))
            propertyObservers[properyName].Remove(listener);
        else
            throw new ArgumentException($"'{properyName}' Not observed");
    }


    private static List<Action<String>> observers = new();
    public static void AddNotifyListener(Action<String> listener)
    {
        observers.Add(listener);
    }
    public static void RemoveNotifyListener(Action<String> listener)
    {
        observers.Remove(listener);
    }
    private static void NotifyListeners([CallerMemberName] String propertyName = "")
    {
        observers.ForEach(listener => listener.Invoke(propertyName));
        if (propertyObservers.ContainsKey(propertyName))
        {
            propertyObservers[propertyName]
                .ForEach(listener => listener.Invoke());
        }
    }


    public static event Action OnSoundsMuteChanged;
    public static event Action OnMusicVolumeChanged;
    public static event Action OnEffectsVolumeChanged;
    public static void SoundsMuteChanged(bool value)
    {
        isSoundsMuted = value;
        OnSoundsMuteChanged?.Invoke();
    }
    public static void MusicVolumeChanged(float value)
    {
        musicVolume = value;
        OnMusicVolumeChanged?.Invoke();
    }
    public static void EffectsVolumeChanged(float value)
    {
        effectsVolume = value;
        OnEffectsVolumeChanged?.Invoke();
    }
}
