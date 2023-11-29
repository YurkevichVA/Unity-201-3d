using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileScript : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    private float forceFactor = 500f;
    private void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");
        Vector3 forceDirection = new Vector3(kh, 0, kv);
        body.AddForce(forceFactor * Time.deltaTime * forceDirection);
    }
}
