using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isMoving = false;
    public int bossHitpoints = 700;

    private float speed;
    private GroundMoveScript groundMoveScript;
    private LogicScript logicScript;
    void Start()
    {
        groundMoveScript = GameObject.FindGameObjectWithTag("Moving").GetComponent<GroundMoveScript>();
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        speed = groundMoveScript.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * speed, transform.position.y);
        }
        if (bossHitpoints <= 0)
        {
            gameObject.SetActive(false);
            logicScript.isGameWon = true;
        }
    }

    public void TakeDamage(int damage)
    {
        bossHitpoints -= damage;
    }
}
