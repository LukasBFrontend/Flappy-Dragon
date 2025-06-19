using System;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public float timer = 0;
    [HideInInspector] public Animator animator;
    [SerializeField] private int bossHitpoints = 700;
    [SerializeField]
    private GameObject missilePrefab, bazookaOne, bazookaTwo, cannonShotPrefab, cannonOne, cannonTwo, firePointOne, firePointTwo, laserGun, missileLaunchers, healthTextObject, healthBar;
    private Animator launcherAnimator;
    private SpriteRenderer launcherRenderer;
    private BossLaser laserScript;
    private GroundMoveScript groundMoveScript;
    private LogicScript logicScript;
    private Text healthText;
    private RectTransform healthBarTransform;
    private float healthBarWidth, healthBarHeight;
    private int maxHitpoints;
    float speed;
    //public float missileInterval = 2f;
    //public float cannonInterval = .7f;

    [SerializeField] private NextAttack[] attacks;
    private float attackTimer = 0;
    private int attackIndex = 0;

    public int HealthInPercent()
    {
        return 100 * bossHitpoints / maxHitpoints;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        launcherAnimator = missileLaunchers.GetComponent<Animator>();
        launcherRenderer = missileLaunchers.GetComponent<SpriteRenderer>();
        healthText = healthTextObject.GetComponent<Text>();
        healthBarTransform = healthBar.GetComponent<RectTransform>();
        laserScript = laserGun.GetComponent<BossLaser>();
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
            if (attackTimer <= .5f)
            {
                AttackType comingAttack = attacks[attackIndex].Attack;
                if (comingAttack == AttackType.MissileOne || comingAttack == AttackType.MissileTwo && !launcherRenderer.enabled)
                {
                    launcherRenderer.enabled = true;
                }

                if (comingAttack != AttackType.MissileOne && comingAttack != AttackType.MissileTwo && launcherRenderer.enabled)
                {
                    launcherRenderer.enabled = false;
                }
            }
            if (attackTimer <= 0)
            {
                switch (attacks[attackIndex].Attack)
                {
                    case AttackType.Cannons:
                        ShootCannons();
                        break;
                    case AttackType.MissileOne:
                        ShootMissileOne();
                        break;
                    case AttackType.MissileTwo:
                        ShootMissileTwo();
                        break;
                    case AttackType.Laser:
                        laserScript.StartLaserSequence();
                        break;
                    default:
                        break;
                }
                attackTimer = attacks[attackIndex].Delay;
                attackIndex = (attackIndex + 1) % attacks.Length;
            }
            timer += Time.deltaTime;
            attackTimer -= Time.deltaTime;
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
        if (cannonOne?.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            Instantiate(cannonShotPrefab, firePointOne.transform);
            cannonOne.transform.DetachChildren();
        }

        if (cannonTwo?.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            Instantiate(cannonShotPrefab, firePointTwo.transform);
            cannonTwo.transform.DetachChildren();
        }
    }

    public void ShootMissileOne()
    {
        Instantiate(missilePrefab, bazookaOne.transform);
        bazookaOne.transform.DetachChildren();
    }

    public void ShootMissileTwo()
    {
        Instantiate(missilePrefab, bazookaTwo.transform);
        bazookaTwo.transform.DetachChildren();
    }

    public void TakeDamage(int damage)
    {
        if (!isMoving) return;

        bossHitpoints -= damage;
        healthBarTransform.sizeDelta = new Vector2(healthBarWidth * bossHitpoints / maxHitpoints, healthBarHeight);
        healthText.text = bossHitpoints.ToString();
        animator.SetInteger("BossHealth", 100 * bossHitpoints / maxHitpoints);
        launcherAnimator.SetInteger("BossHealth", 100 * bossHitpoints / maxHitpoints);
    }
}
[System.Serializable]
public class NextAttack
{
    public AttackType Attack;
    [Range(0f, 5f)] public float Delay;
}

public enum AttackType
{
    Cannons,
    MissileOne,
    MissileTwo,
    Laser
};
