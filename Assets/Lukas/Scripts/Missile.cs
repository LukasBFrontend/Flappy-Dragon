using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool isMoving = false;
    public bool targetsPlayer = false;
    public int speed = 3;
    private GameObject player;
    private bool targetIsSet = false;
    private Vector2 targetPosition;
    private float targetAngle = 0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (targetsPlayer)
            {
                if (!targetIsSet)
                {
                    targetIsSet = true;
                    targetPosition = player.transform.position;

                    float deltaY = transform.position.y - targetPosition.y;
                    float deltaX = transform.position.x - targetPosition.x;
                    targetAngle = deltaY / deltaX;
                }
            }
            transform.position = new Vector2(transform.position.x - Time.deltaTime * speed, transform.position.y - Time.deltaTime * targetAngle * speed);
        }
    }
}
