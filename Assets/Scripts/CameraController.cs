using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float minX, maxX, minY, maxY;

    void Update()
    {
        float clampedX = Mathf.Clamp(cameraTransform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(cameraTransform.position.y, minY, maxY);
        cameraTransform.position = new Vector3(clampedX, clampedY, cameraTransform.position.z);
    }
}

