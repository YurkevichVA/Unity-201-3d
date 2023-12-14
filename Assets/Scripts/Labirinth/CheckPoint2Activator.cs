using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2Activator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint 2 activator Trigger enter " + other.name);
        LabirinthState.isCheckPoint2ActivatorPassed = true;
        Destroy(gameObject);
    }
}
