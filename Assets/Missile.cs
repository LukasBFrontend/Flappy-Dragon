using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector]
    public bool isMoving = false;
    public int speed = 3;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * speed, transform.position.y);
        }
    }
}
