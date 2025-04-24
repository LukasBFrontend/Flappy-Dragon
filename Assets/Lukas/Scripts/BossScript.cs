using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public bool isMoving = false;
    public int bossHitpoints = 700;
    private int maxHitpoints;
    private float speed;
    public float timer = 0;
    private float missileInterval = 2f;
    private float cannonInterval = 1f;
    private bool missileOneFired, missileTwoFired, cannonOneFired, cannonTwoFired = false;
    [SerializeField]
    private GameObject missilePrefab, bazookaOne, bazookaTwo, cannonShotPrefab, cannonOne, cannonTwo, healthTextObject, healthBar;
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
        if (timer % cannonInterval >= cannonInterval / 2 && !cannonOneFired)
        {
            Instantiate(cannonShotPrefab, cannonOne.transform);
            cannonOneFired = true;
        }
        else if (timer % cannonInterval < cannonInterval / 2)
        {
            cannonOneFired = false;
        }

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
        bossHitpoints -= damage;
        healthBarTransform.sizeDelta = new Vector2(healthBarWidth * bossHitpoints / maxHitpoints, healthBarHeight);
        healthText.text = bossHitpoints.ToString();
    }
}
