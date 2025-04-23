using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isMoving = false;
    public int bossHitpoints = 700;
    private int maxHitpoints;

    private float speed;
    public GameObject healthTextObject;
    public GameObject healthBar;
    private Text healthText;
    private RectTransform healthBarTransform;
    private float healthBarWidth;
    private float healthBarHeight;
    private GroundMoveScript groundMoveScript;
    private LogicScript logicScript;
    void Start()
    {
        healthText = healthTextObject.GetComponent<Text>();
        healthBarTransform = healthBar.GetComponent<RectTransform>();
        groundMoveScript = GameObject.FindGameObjectWithTag("Moving").GetComponent<GroundMoveScript>();
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        healthBarWidth = healthBarTransform.sizeDelta.x;
        healthBarHeight = healthBarTransform.sizeDelta.y;
        maxHitpoints = bossHitpoints;
        speed = groundMoveScript.moveSpeed;

        healthText.text = bossHitpoints.ToString();
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
            ScreenManager.Instance.HideBossCanvas();
            gameObject.SetActive(false);
            logicScript.GameWon();
        }
    }

    public void TakeDamage(int damage)
    {
        bossHitpoints -= damage;
        healthBarTransform.sizeDelta = new Vector2(healthBarWidth * bossHitpoints / maxHitpoints, healthBarHeight);
        healthText.text = bossHitpoints.ToString();
    }
}
