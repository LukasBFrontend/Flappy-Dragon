using UnityEngine;

public class PixelPositionDebug : MonoBehaviour
{
    public float pixelsPerUnit = 32f;
    private Vector3 lastPosition;

    void Update()
    {
        Vector3 pos = transform.position;

        // Snap to pixel grid for comparison
        float snappedX = Mathf.Round(pos.x * pixelsPerUnit) / pixelsPerUnit;
        float snappedY = Mathf.Round(pos.y * pixelsPerUnit) / pixelsPerUnit;

        // Only log if the position changed
        if (pos != lastPosition)
        {
            Debug.Log($"[Pixel Debug] Pos: ({pos.x:F5}, {pos.y:F5}) | " +
                      $"Snapped: ({snappedX:F5}, {snappedY:F5}) | " +
                      $"Delta: ({(pos.x - snappedX):F5}, {(pos.y - snappedY):F5})");

            lastPosition = pos;
        }
    }
}
