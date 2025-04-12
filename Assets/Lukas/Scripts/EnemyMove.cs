using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum travelAxis { X, Y, none }
    public enum pathCurve { PingPong, Sinus, none };
    public travelAxis selectedAxis;
    public pathCurve selectedCurve;
    public float distance = 2;
    public float oscillationSpeed = 2;
    public float travelSpeed = 2;
    private float min = 0f;
    private float max = 0f;

    private float time;

    void Start()
    {
        if (selectedAxis == travelAxis.X)
        {
            min = transform.position.x;
            max = transform.position.x + distance;
        }

        if (selectedAxis == travelAxis.Y)
        {
            min = transform.position.y;
            max = transform.position.y + distance;
        }
    }

    void Update()
    {
        time = Time.timeSinceLevelLoad;
        if (selectedAxis == travelAxis.Y)
        {
            if (selectedCurve == pathCurve.PingPong)
            {
                transform.localPosition = new Vector2(Mathf.PingPong(time * oscillationSpeed, max - min), transform.localPosition.y + Time.deltaTime * -travelSpeed);
            }
            else if (selectedCurve == pathCurve.Sinus)
            {
                transform.localPosition = new Vector2(Mathf.Cos(time * oscillationSpeed) * (max - min), transform.localPosition.y + Time.deltaTime * -travelSpeed);
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
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.PingPong(time * oscillationSpeed, max - min));
            }
            else if (selectedCurve == pathCurve.Sinus)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, Mathf.Cos(time * oscillationSpeed) * (max - min));
            }
            else
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime * -travelSpeed, transform.localPosition.y);
            }
        }
    }
}
