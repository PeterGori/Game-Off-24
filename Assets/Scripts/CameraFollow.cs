using System;
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    public float Camera_worldSwitch_Y_Value;
    public int Camera_goodWorld;  // 1 = goodworld
    public bool CameraSwitch_active = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && CameraSwitch_active == false)
        {
            StartCoroutine(CameraWorldSwitchCoroutine());
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
       
    }

    public IEnumerator CameraWorldSwitchCoroutine()
    {
        CameraSwitch_active = true;
        yield return new WaitForSeconds(0.4f);
        
        if (Camera_goodWorld == 1)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += Camera_worldSwitch_Y_Value;
            transform.position = newPosition;
            Camera_goodWorld = 0;
        }
        else if (Camera_goodWorld == 0)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= Camera_worldSwitch_Y_Value;
            transform.position = newPosition;
            Camera_goodWorld = 1;
        }
        
        yield return new WaitForSeconds(1.5f);
        CameraSwitch_active = false;
    }
}
