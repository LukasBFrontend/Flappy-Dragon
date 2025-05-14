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
    float speed;
    public float missileInterval = 2f;
    public float cannonInterval = .7f;
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
        if (cannonOne && cannonOne.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            if (timer % cannonInterval >= cannonInterval / 2 && !cannonOneFired)
            {
                Instantiate(cannonShotPrefab, cannonOne.transform);
                cannonOne.transform.DetachChildren();
                cannonOneFired = true;
            }
            else if (timer % cannonInterval < cannonInterval / 2)
            {
                cannonOneFired = false;
            }
        }

        if (cannonTwo && cannonTwo.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            if (timer % cannonInterval >= cannonInterval / 2 && !cannonTwoFired)
            {
                Instantiate(cannonShotPrefab, cannonTwo.transform);
                cannonTwo.transform.DetachChildren();
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
            bazookaOne.transform.DetachChildren();
            missileOneFired = true;

            Instantiate(missilePrefab, bazookaTwo.transform);
            bazookaTwo.transform.DetachChildren();
            missileTwoFired = true;
        }
        else if (timer % missileInterval < missileInterval / 2)
        {
            missileOneFired = false;
            missileTwoFired = false;
        }

        /*         if (timer % missileInterval <= missileInterval / 2 && !missileTwoFired)
                {
                    Instantiate(missilePrefab, bazookaTwo.transform);
                    bazookaTwo.transform.DetachChildren();
                    missileTwoFired = true;
                }
                else if (timer % missileInterval > missileInterval / 2)
                {
                    missileTwoFired = false;
                } */
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
