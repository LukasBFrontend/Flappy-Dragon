using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private enum travelAxis { X, Y, none }
    [SerializeField] private enum pathCurve { PingPong, Sinus, none };
    [SerializeField] private travelAxis selectedAxis;
    [SerializeField] private pathCurve selectedCurve;
    public float distance, oscillationSpeed, travelSpeed = 2;
    private float min = 0f;
    private float max = 0f;
    private float time;

    void Start()
    {
        if (selectedAxis == travelAxis.Y)
        {
            min = transform.localPosition.x;
            max = transform.localPosition.x + distance;
        }

        if (selectedAxis == travelAxis.X)
        {
            min = transform.localPosition.y;
            max = transform.localPosition.y + distance;
        }
    }

    void Update()
    {
        time = Time.timeSinceLevelLoad;
        if (selectedAxis == travelAxis.Y)
        {
            if (selectedCurve == pathCurve.PingPong)
            {
                transform.localPosition = new Vector2(Mathf.PingPong(time * oscillationSpeed, max - min) + min, transform.localPosition.y + Time.deltaTime * -travelSpeed);
            }
            else if (selectedCurve == pathCurve.Sinus)
            {
                transform.localPosition = new Vector2(Mathf.Cos(time * oscillationSpeed) * (max - min) + min, transform.localPosition.y + Time.deltaTime * -travelSpeed);
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
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.PingPong(time * oscillationSpeed, max - min) + min);
            }
            else if (selectedCurve == pathCurve.Sinus)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.Cos(time * oscillationSpeed) * (max - min) + min);
            }
            else
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, transform.localPosition.y);
            }
        }
    }
}
