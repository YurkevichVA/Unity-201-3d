using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileScript : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    [SerializeField] private Rigidbody body;
    
    private float forceFactor = 500f;
    private void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");

        Vector3 right = _camera.transform.right;
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 forceDirection = // new Vector3(kh, 0, kv);
            kh * right + kv * forward;

        body.AddForce(forceFactor * Time.deltaTime * forceDirection.normalized);
    }
}
