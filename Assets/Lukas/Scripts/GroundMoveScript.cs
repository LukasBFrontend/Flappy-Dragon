using UnityEngine;

public class GroundMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public PlayerScript playerScript;
    public LogicScript logicScript;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (playerScript.playerIsAlive && !logicScript.isGameWon)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        }
    }
}
