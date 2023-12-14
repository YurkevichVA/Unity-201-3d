using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates1Script : MonoBehaviour
{
    private float swingPeriod = 0.3f;
    void Start()
    {
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint1Passed), OnCheckPoint1Passed);
    }

    void Update()
    {
        float factor = Time.deltaTime / swingPeriod;

        #region trash
        //if (!LabirinthState.checkPoint1Passed)
        //{
        //    factor *= 10f;
        //}

        //transform.Translate(factor * Vector3.down);
        //if (transform.position.y < -0.35)
        //{
        //    transform.position = new Vector3(transform.position.x, -0.35f, transform.position.z);
        //    swingPeriod = -swingPeriod;
        //}
        //if (transform.position.y > 0.18)
        //{
        //    transform.position = new Vector3(transform.position.x, 0.18f, transform.position.z);
        //    swingPeriod = -swingPeriod;
        //}
        #endregion

        Vector3 translateDirection = factor * Vector3.down;
        Vector3 newPosition = transform.position + translateDirection;
        
        if (newPosition.y <= -0.35f || newPosition.y >= 0.18f)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, -0.35f, 0.18f);
            swingPeriod = -swingPeriod;
        }
        
        transform.position = newPosition;
    }

    private void OnCheckPoint1Passed()
    {
        swingPeriod = 3f;
    }
}
