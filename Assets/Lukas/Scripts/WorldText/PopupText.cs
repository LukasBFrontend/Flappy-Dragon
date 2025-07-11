using UnityEngine;

public class PopupText : MonoBehaviour
{
    public bool moveWithLvl;
    [SerializeField] private float timer = 2f;
    private GroundMoveScript moveScript;
    private float moveSpeed;
    private float verticalSpeed = 1;
    void Start()
    {
        moveScript = GameObject.FindGameObjectWithTag("Moving").GetComponent<GroundMoveScript>();
        moveSpeed = moveScript.moveSpeed;
    }

    void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

        float posX = moveWithLvl ? transform.position.x - moveSpeed * Time.deltaTime : transform.position.x;
        float posY = transform.position.y + verticalSpeed * Time.deltaTime;
        transform.position = new Vector2(posX, posY);

        timer -= Time.deltaTime;
    }
}
