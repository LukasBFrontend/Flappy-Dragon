using System;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public float timer = 0;
    [HideInInspector] public Animator animator;
    [SerializeField] private int bossHitpoints = 700;
    [SerializeField] private NextAttack[] attacks;
    [Header("Audio")]
    [SerializeField] private SoundFXClip cannonClip;
    [SerializeField] private SoundFXClip laserChargeClip;
    [SerializeField] private SoundFXClip laserClip;
    [SerializeField] private SoundFXClip scream1Clip;
    [SerializeField] private SoundFXClip scream2Clip;
    [SerializeField] private SoundFXClip scream3Clip;
    [SerializeField] private SoundFXClip explosionClip;

    [SerializeField]
    private GameObject missilePrefab, bazookaOne, bazookaTwo, cannonShotPrefab, cannonOne, cannonTwo, firePointOne, firePointTwo, laserGun, missileLaunchers, healthTextObject, healthBar;
    private AudioSource backgroundMusic;
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
    private bool bossStarted, isSecondPhase = false;
    private float deathTimer = 3f;
    private float backgroundStartVolume;
    //public float missileInterval = 2f;
    //public float cannonInterval = .7f;


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
        backgroundMusic = GameObject.FindGameObjectWithTag("BossMusic").GetComponent<AudioSource>();

        healthBarWidth = healthBarTransform.sizeDelta.x;
        healthBarHeight = healthBarTransform.sizeDelta.y;
        maxHitpoints = bossHitpoints;
        speed = groundMoveScript.moveSpeed;
        healthText.text = bossHitpoints.ToString();
        backgroundStartVolume = backgroundMusic.volume;
    }

    void Update()
    {
        if (isMoving)
        {
            if (!isSecondPhase && bossHitpoints <= 500)
            {
                SoundFXManager.Instance.playSoundFXClip(scream2Clip.audioClip, transform, scream2Clip.volume);
                isSecondPhase = true;
            }

            if (!bossStarted)
            {
                SoundFXManager.Instance.playSoundFXClip(scream1Clip.audioClip, transform, scream1Clip.volume);
                bossStarted = true;
            }
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
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            ScreenManager.Instance.HideBossCanvas();
            ScreenManager.Instance.HidePlayerCanvas();

            if (isMoving) SoundFXManager.Instance.playSoundFXClip(scream3Clip.audioClip, transform, scream3Clip.volume);

            isMoving = false;

            float t = 1f - (deathTimer / 3f);
            backgroundMusic.volume = backgroundStartVolume * Mathf.Pow(10f, -2f * t);
            LogicScript.Instance.isGameWon = true;

            if (deathTimer <= 0)
            {
                if (backgroundMusic.enabled) SoundFXManager.Instance.playSoundFXClip(explosionClip.audioClip, transform, explosionClip.volume);
                backgroundMusic.enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>();

                foreach (SpriteRenderer sprite in childSprites) sprite.enabled = false;

            }

            if (deathTimer <= -2)
            {
                Debug.Log("deathTimer <= -2");
                logicScript.GameWon();
                gameObject.SetActive(false);
            }


            deathTimer -= Time.deltaTime;
        }
    }

    public void ShootCannons()
    {
        if (cannonOne?.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            Instantiate(cannonShotPrefab, firePointOne.transform);
            firePointOne.transform.DetachChildren();
            SoundFXManager.Instance.playSoundFXClip(cannonClip.audioClip, transform, cannonClip.volume);
        }

        if (cannonTwo?.GetComponent<BossWeapon>().weaponHitpoints > 0)
        {
            Instantiate(cannonShotPrefab, firePointTwo.transform);
            firePointTwo.transform.DetachChildren();
            SoundFXManager.Instance.playSoundFXClip(cannonClip.audioClip, transform, cannonClip.volume);
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
