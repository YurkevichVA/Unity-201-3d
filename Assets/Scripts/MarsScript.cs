using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsScript : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject surface;
    [SerializeField] private float dayPeriod = 24f;
    [SerializeField] private float skyPeriod = 12f;
    [SerializeField] private float yearPeriod = 365f;
    [SerializeField] private float coef = 360f;
    private void Update()
    {
        surface.transform.Rotate(Vector3.up, Time.deltaTime / (dayPeriod / coef), Space.Self);

        transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / (yearPeriod / coef));
    }
}
