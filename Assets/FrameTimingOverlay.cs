using UnityEngine;

public class FrameTimingOverlay : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style;
    Rect rect;

    void Start()
    {

        rect = new Rect(10, 10, 200, 40);
        style = new GUIStyle();
        style.fontSize = 18;
        style.normal.textColor = Color.white;
    }

    void Update()
    {
        // Use exponential moving average for stability
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = $"FPS: {fps:F1} ({msec:F1} ms)";
        GUI.Label(rect, text, style);
    }
}
