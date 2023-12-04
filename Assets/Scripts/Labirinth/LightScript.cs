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
