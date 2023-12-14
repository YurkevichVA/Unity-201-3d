using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates2 : MonoBehaviour
{
    private float period = 100f / 360f;
    void Start()
    {
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint2Passed), OnCheckPoint2Passed);   
    }
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime / period);
    }
    private void OnCheckPoint2Passed()
    {
        if (LabirinthState.checkPoint2Passed)
            period /= 10f;
    }
    private void OnDestroy()
    {
        LabirinthState.RemovePropertyListener(nameof(LabirinthState.checkPoint2Passed), OnCheckPoint2Passed);
    }
}
