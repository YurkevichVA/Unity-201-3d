using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private bool _isDay;
    private bool isDay
    {
        get => _isDay;
        set
        {
            LabirinthState.isDay = _isDay = value;
            if (_isDay) SetDayLighting();
            else SetNightLighting();
        }
    }
    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        isDay = true;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            isDay = !isDay;
        }

        if (Input.GetKey(KeyCode.Equals) && lightComponent.intensity < 1f)
        {
            float intensity = lightComponent.intensity + 0.01f;
            if (intensity >= 1f) lightComponent.intensity = 1f;
            else lightComponent.intensity = intensity;
        }
        if (Input.GetKey(KeyCode.Minus) && lightComponent.intensity > 0.01f)
        {
            float intensity = lightComponent.intensity - 0.01f;
            if (intensity <= 0.01f) lightComponent.intensity = 0.01f;
            else lightComponent.intensity = intensity;
        }
    }

    private void SetDayLighting()
    {
        lightComponent.intensity = 1f;
    }
    private void SetNightLighting()
    {
        lightComponent.intensity = 0.1f;
    }
}
/* Управління світлом
 * 1. Спрямоване світло - основне джерело освітлення, не залежить від позиції, тільки повороти (нахил) променів світла. Головна характеристика для управління - інтенсивність
 * 
 */
