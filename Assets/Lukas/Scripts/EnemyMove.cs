using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private enum travelAxis { X, Y, none }
    [SerializeField] private enum pathCurve { PingPong, Sinus, none };
    [SerializeField] private travelAxis selectedAxis;
    [SerializeField] private pathCurve selectedCurve;
    [SerializeField][Range(-1f, 1f)] private float phaseOffset = 0f;
    [HideInInspector] public bool isMoving = false;
    public float distance, oscillationSpeed, travelSpeed = 2;
    private float min = 0f;
    private float max = 0f;
    private float time;
    private bool activated = false;
    private float lengthOffset;

    void Start()
    {
        if (selectedAxis == travelAxis.Y)
        {
            min = transform.localPosition.x - distance / 2;
            max = transform.localPosition.x + distance / 2;
        }

        if (selectedAxis == travelAxis.X)
        {
            min = transform.localPosition.y - distance / 2;
            max = transform.localPosition.y + distance / 2;
        }

        lengthOffset = phaseOffset * distance;
    }

    void Update()
    {
        if (transform.position.x <= 12 && !activated)
        {
            isMoving = true;
            activated = true;
        }

        if (!isMoving) return;

        time = Time.timeSinceLevelLoad;
        Vector2 pos = transform.localPosition;

        switch (selectedAxis)
        {
            case travelAxis.Y:
                pos.y += Time.deltaTime * -travelSpeed;
                switch (selectedCurve)
                {
                    case pathCurve.PingPong:
                        pos.x = Mathf.PingPong(time * oscillationSpeed + lengthOffset, max - min) + min;
                        break;
                    case pathCurve.Sinus:
                        pos.x = Mathf.Cos(time * oscillationSpeed + phaseOffset) * (max - min) + pos.x;
                        break;
                }
                break;

            case travelAxis.X:
                pos.x += Time.deltaTime * -travelSpeed;
                switch (selectedCurve)
                {
                    case pathCurve.PingPong:
                        pos.y = Mathf.PingPong(time * oscillationSpeed + lengthOffset, max - min) + min;
                        break;
                    case pathCurve.Sinus:
                        pos.y = Mathf.Cos(time * oscillationSpeed + phaseOffset * Mathf.PI) * (max - min) / 2 + min + distance / 2;
                        break;
                }
                break;
        }

        transform.localPosition = pos;
    }
}
