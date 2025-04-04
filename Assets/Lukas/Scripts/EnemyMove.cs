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
        if (selectedAxis == travelAxis.Y)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, Mathf.PingPong(Time.time * oscillationSpeed, max - min) + min);
        }
        else if (selectedAxis == travelAxis.X)
        {
            transform.localPosition = new Vector2(Mathf.PingPong(Time.time * oscillationSpeed, max - min) + min, transform.localPosition.y);
        }
        else
        {
            if (selectedCurve == pathCurve.Sinus)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime, MathF.Cos(Time.time * oscillationSpeed));
            }
            else if (selectedCurve == pathCurve.PingPong)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + Time.deltaTime, Mathf.PingPong(Time.time * oscillationSpeed, max - min) + min);
            }
        }


    }
}
