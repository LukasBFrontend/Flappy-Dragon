using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public float timer = 0;
    [HideInInspector] public Animator animator;
    [SerializeField] private int bossHitpoints = 700;
    [SerializeField]
    private GameObject missilePrefab, bazookaOne, bazookaTwo, cannonShotPrefab, cannonOne, cannonTwo, healthTextObject, healthBar;
    private GroundMoveScript groundMoveScript;
    private LogicScript logicScript;
    private Text healthText;
    private RectTransform healthBarTransform;
    private float healthBarWidth, healthBarHeight;
    private int maxHitpoints;
    private float speed;
    public float missileInterval = 2f;
    public float cannonInterval = 1f;
    private bool missileOneFired, missileTwoFired, cannonOneFired, cannonTwoFired = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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

    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;

            ShootMissiles();
            ShootCannons();

            transform.position = new Vector2(transform.position.x + Time.deltaTime * speed, transform.position.y);
        }
        if (bossHitpoints <= 0)
        {
            ScreenManager.Instance.HideBossCanvas();
            gameObject.SetActive(false);
            logicScript.GameWon();
        }
    }

    public void ShootCannons()
    {
        if (cannonOne)
        {
            if (timer % cannonInterval >= cannonInterval / 2 && !cannonOneFired)
            {
                Instantiate(cannonShotPrefab, cannonOne.transform);
                cannonOneFired = true;
            }
            else if (timer % cannonInterval < cannonInterval / 2)
            {
                cannonOneFired = false;
            }
        }

        if (cannonTwo)
        {
            if (timer % cannonInterval >= cannonInterval / 2 && !cannonTwoFired)
            {
                Instantiate(cannonShotPrefab, cannonTwo.transform);
                cannonTwoFired = true;
            }
            else if (timer % cannonInterval < cannonInterval / 2)
            {
                cannonTwoFired = false;
            }
        }
    }

    public void ShootMissiles()
    {
        if (timer % missileInterval >= missileInterval / 2 && !missileOneFired)
        {
            Instantiate(missilePrefab, bazookaOne.transform);
            missileOneFired = true;
        }
        else if (timer % missileInterval < missileInterval / 2)
        {
            missileOneFired = false;
        }

        if (timer % missileInterval <= missileInterval / 2 && !missileTwoFired)
        {
            Instantiate(missilePrefab, bazookaTwo.transform);
            missileTwoFired = true;
        }
        else if (timer % missileInterval > missileInterval / 2)
        {
            missileTwoFired = false;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("BossScript.TakeDamage called");
        bossHitpoints -= damage;
        healthBarTransform.sizeDelta = new Vector2(healthBarWidth * bossHitpoints / maxHitpoints, healthBarHeight);
        healthText.text = bossHitpoints.ToString();
        animator.SetInteger("BossHealth", 100 * bossHitpoints / maxHitpoints);
    }
}
