using UnityEngine;

public class Initializer : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 1; // Use VSync to sync with monitor
        Application.targetFrameRate = 60; // Match your display refresh rate
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
}
