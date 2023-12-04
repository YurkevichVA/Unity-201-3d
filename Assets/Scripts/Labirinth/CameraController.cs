using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraAnchor;

    private float camAngleX;
    private float camAngleY;
    private float rodAngleX;
    private float rodAngleY;
    private Vector3 camRod;

    private void Start()
    {
        camAngleX = transform.eulerAngles.x;
        camAngleY = transform.eulerAngles.y;
        camRod = transform.position;
        rodAngleX = rodAngleY = 0f;
    }
    private void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        if (camAngleX - my > 60 && !LabirinthState.cameraFirstPerson)
        {
            camAngleX = 60;
        }
        else if(camAngleX - my < 35 && !LabirinthState.cameraFirstPerson)
        {
            camAngleX = 35;
        }
        else
        {
            camAngleX -= my;
            
            rodAngleX -= my;
        }

        camAngleY += mx;

        rodAngleY += mx;

        if (Input.GetKeyDown(KeyCode.V))
        {
            LabirinthState.cameraFirstPerson =
                !LabirinthState.cameraFirstPerson;
        }
    }
    private void LateUpdate()
    {
        if (LabirinthState.cameraFirstPerson)
        {
            transform.eulerAngles = new Vector3(camAngleX, camAngleY, 0f);
            transform.position = cameraAnchor.transform.position;
        }
        else
        {
            if (camAngleX > 60)
            {
                camAngleX = 60;
                rodAngleX = 0;
            }
            if (camAngleX < 35)
            {
                camAngleX = 35;
                rodAngleX = 0;
            }

            transform.eulerAngles = new Vector3(camAngleX, camAngleY, 0f);
            transform.position =
                Quaternion.Euler(rodAngleX, rodAngleY, 0) * camRod;
        }
    }
}
