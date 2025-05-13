using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [HideInInspector] public bool isMoving, targetsPlayer, targetIsSet = false;
    private GameObject player;
    private Vector2 targetPosition;
    private float targetAngle = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
