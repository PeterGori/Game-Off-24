using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    public float Camera_worldSwitch_Y_Value;
    public int Camera_goodWorld;  // 1 = goodworld

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            CameraWorldSwitch();
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
    public void CameraWorldSwitch()
    {
        {
            if (Camera_goodWorld == 1)
            {
                Vector3 newPosition = transform.position;
                newPosition.y += Camera_worldSwitch_Y_Value;
                transform.position = newPosition;
                Camera_goodWorld = 0;
            }
            else
            {
                Vector3 newPosition = transform.position;
                newPosition.y -= Camera_worldSwitch_Y_Value;
                transform.position = newPosition;
                Camera_goodWorld = 1;
            }
        }
    }
}
