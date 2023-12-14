using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint2 : MonoBehaviour
{
    [SerializeField] private Image indicator;

    private bool isActiveted;

    private float checkPointTimeout = 10f;
    void Start()
    {
        LabirinthState.AddPropertyListener(nameof(LabirinthState.isCheckPoint2ActivatorPassed), Activate);
        LabirinthState.checkPoint2Amount = 1f;
        LabirinthState.checkPoint2Passed = false;
    }

    void Update()
    {
        if (isActiveted)
        {
            LabirinthState.checkPoint2Amount -= Time.deltaTime / checkPointTimeout;
            if (LabirinthState.checkPoint2Amount > 0f)
            {
                indicator.fillAmount = LabirinthState.checkPoint2Amount;
                indicator.color = new Color(
                    1f - indicator.fillAmount,
                    indicator.fillAmount,
                    0.3f
                );
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint 2 Trigger enter " + other.name);
        LabirinthState.checkPoint2Passed = true;
        Destroy(gameObject);
    }
    private void Activate()
    {
        isActiveted = true;
    }
    private void OnDestroy()
    {
        LabirinthState.RemovePropertyListener(nameof(LabirinthState.isCheckPoint2ActivatorPassed), Activate);
    }
}
