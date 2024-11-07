using UnityEngine;

public class FrameRate : MonoBehaviour

{

    public int FPS = 60;

    void Start()

    {

        QualitySettings.vSyncCount = 1;

        Application.targetFrameRate = FPS;

    }

} 
