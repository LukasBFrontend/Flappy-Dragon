using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum travelAxis { X, Y, none }
    public enum pathCurve { PingPong, Sinus, none };
    public travelAxis selectedAxis;
    public pathCurve selectedCurve;
    float min = 0f;
    float max = 0f;
    public float distance = 2;
    public float oscillationSpeed = 2;
    public float travelSpeed = 2;

    private float time;
    // Use this for initialization
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

    // Update is called once per frame
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
