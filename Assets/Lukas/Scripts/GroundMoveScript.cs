using UnityEngine;

public class GroundMoveScript : MonoBehaviour
{
    public float moveSpeed = 4.6875f; // units per second
    private PlayerScript playerScript;
    private LogicScript logicScript;

    private float positionX = 0f;
    private float accumulatedX = 0f;
    private const float increment = 64f; // pixels per unit

    void Start()
    {
        positionX = transform.position.x;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!playerScript.playerIsAlive || logicScript.isGameWon)
            return;

        accumulatedX -= moveSpeed * Time.deltaTime;
        float stepSize = 1f / increment;

        while (Mathf.Abs(accumulatedX) >= stepSize)
        {
            float step = Mathf.Sign(accumulatedX) * stepSize;
            positionX += step;
            accumulatedX -= step;
        }

        float pixelSize = 1f / 64f;
        float roundedX = Mathf.Round(positionX / pixelSize) * pixelSize;

        transform.position = new Vector3(roundedX, transform.position.y, transform.position.z);
    }
}
