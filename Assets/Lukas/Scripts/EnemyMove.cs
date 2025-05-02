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
        time = Time.timeSinceLevelLoad;

        if (isMoving)
        {
            if (selectedAxis == travelAxis.Y)
            {
                if (selectedCurve == pathCurve.PingPong)
                {
                    transform.localPosition = new Vector2(Mathf.PingPong(time * oscillationSpeed + lengthOffset, max - min) + min, transform.localPosition.y + Time.deltaTime * -travelSpeed);
                }
                else if (selectedCurve == pathCurve.Sinus)
                {
                    transform.localPosition = new Vector2(Mathf.Cos(time * oscillationSpeed + phaseOffset) * (max - min) + transform.localPosition.x, transform.localPosition.y + Time.deltaTime * -travelSpeed);
                }
                else
                {
                    transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * -travelSpeed);
                }
            }
            else if (selectedAxis == travelAxis.X)
            {
                if (selectedCurve == pathCurve.PingPong)
                {
                    transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.PingPong(time * oscillationSpeed + lengthOffset, max - min) + min);
                }
                else if (selectedCurve == pathCurve.Sinus)
                {
                    transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.Cos(time * oscillationSpeed + phaseOffset * Mathf.PI) * (max - min) / 2 + min + distance / 2);
                }
                else
                {
                    transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, transform.localPosition.y);
                }
            }
        }
    }
}
