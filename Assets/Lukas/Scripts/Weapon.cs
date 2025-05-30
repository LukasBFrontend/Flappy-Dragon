using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AudioClip[] fireClips;
    [SerializeField] private AudioClip laserClip;
    [SerializeField][Range(0, 100)] private float audioVolume = 1f;
    [SerializeField] private GameObject ammoMeter;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject laser, laserVortex, laserImpact;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private Texture[] textures;
    [SerializeField] private float chargeDuration = 1f;
    [SerializeField] private float fps = 30f;
    [SerializeField] private float animationDuration = .5f;
    private RectTransform ammoMeterTransform;
    private Animator laserVortexAnimator;
    private float ammoMeterHeight;
    private float ammoMeterWidth;
    private float maxAmmo = 5f;
    private float currentAmmo;
    private int laserTicks = 10;
    private int laserTickCount = 0;
    private float rechargeDelay = 0.3f;
    private float rechargeTimer = 0f;
    private float rechargeRate = 4f;
    private int animationStep = 0;
    private float laserRecoveryTimer = 1f;
    private float fpsCounter, animationTimer, chargeTimer, groundMoveSpeed;
    private bool animationIsActive = false;
    private bool playerIsAlive;
    private PlayerScript playerScript;
    private LineRenderer laserRenderer;
    void Start()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        groundMoveSpeed = GameObject.FindGameObjectWithTag("Moving").GetComponent<GroundMoveScript>().moveSpeed;
        laserVortexAnimator = laserVortex.GetComponent<Animator>();

        playerIsAlive = playerScript.playerIsAlive;
        chargeTimer = chargeDuration;
        animationTimer = animationDuration;

        ammoMeterTransform = ammoMeter.GetComponent<RectTransform>();
        currentAmmo = maxAmmo;
        ammoMeterWidth = ammoMeterTransform.sizeDelta.x;
        ammoMeterHeight = ammoMeterTransform.sizeDelta.y;
    }

    void Awake()
    {
        laserRenderer = laser.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (laserRecoveryTimer < 1f)
        {
            laserRecoveryTimer += Time.deltaTime / 4;
        }
        rechargeTimer -= Time.deltaTime;
        if (currentAmmo < maxAmmo && rechargeTimer < 0 && LogicScript.Instance.LvlIsActive())
        {
            currentAmmo += Time.deltaTime * rechargeRate * laserRecoveryTimer;
            SetAmmoBar();
        }


        playerIsAlive = playerScript.playerIsAlive;

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
        {
            chargeTimer = chargeDuration;
        }
        if ((Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Return)) && playerIsAlive && !LogicScript.Instance.isPaused && !EventSystem.current.IsPointerOverGameObject())
        {
            laserVortex.GetComponent<SpriteRenderer>().enabled = false;
            laserVortexAnimator.enabled = false;

            if (chargeTimer > chargeDuration - .3f)
            {
                Shoot();
            }
            else if (playerScript.activePowerUp == PowerUp.Blue)
            {
                ShootLaser();
            }

        }
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Return))
        {
            chargeTimer -= Time.deltaTime;

            if (chargeTimer <= chargeDuration - .3f)
            {
                laserVortex.GetComponent<SpriteRenderer>().enabled = true;
                laserVortexAnimator.enabled = true;
                laserVortexAnimator.SetBool("IsCharging", true);
            }
        }


        if (animationIsActive)
        {
            animationTimer -= Time.deltaTime;
            fpsCounter += Time.deltaTime;

            if (fpsCounter >= 1f / fps)
            {
                if (animationStep == textures.Length)
                {
                    animationStep = 0;
                }
                if (animationStep < textures.Length)
                {
                    laserRenderer.material.SetTexture("_MainTex", textures[animationStep]);
                }

                animationStep++;

                fpsCounter = 0f;
            }
        }
        if (animationTimer < 0)
        {
            animationIsActive = false;
            laserRenderer.enabled = false;
            laserImpact.SetActive(false);
            laserTickCount = 0;
        }
        else if (animationIsActive)
        {
            UpdateLaser();
        }
    }

    private void SetAmmoBar()
    {
        ammoMeterTransform.sizeDelta = new Vector2(ammoMeterWidth * currentAmmo / maxAmmo, ammoMeterHeight);
    }

    void Shoot()
    {
        if (currentAmmo >= 1)
        {
            rechargeTimer = rechargeDelay;
            currentAmmo--;
            SetAmmoBar();

            int random = Random.Range(0, 3);
            SoundFXManager.Instance.playSoundFXClip(fireClips[random], transform, audioVolume);
            Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        }
    }

    void ShootLaser()
    {
        if (currentAmmo >= 4f)
        {
            SoundFXManager.Instance.playSoundFXClip(laserClip, transform, audioVolume);
            laserRecoveryTimer = 0f;
            currentAmmo -= 4f;
            rechargeTimer = 0.6f;
            SetAmmoBar();

            laserRenderer.enabled = true;
            animationIsActive = true;
            animationTimer = animationDuration;
            animationStep = 0;
            fpsCounter = 0;

            laserImpact.SetActive(true);

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, new Vector2(1f, 0f), 50f, layersToHit);
            if (hit.collider != null)
            {
                laserImpact.transform.localPosition = new Vector3(hit.distance + 0.5f, 0f, 0f);
                int damage = 100;
                Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                BossWeapon bossWeapon = hit.transform.gameObject.GetComponent<BossWeapon>();
                BossScript bossScript = hit.transform.gameObject.GetComponent<BossScript>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                else if (bossWeapon != null)
                {
                    bossWeapon.TakeDamage(damage);
                }
                else if (bossScript != null)
                {
                    bossScript.TakeDamage(damage);
                }
                laserRenderer.SetPosition(1, new Vector3(hit.distance + 0.5f, 0f, 0f));

                return;
            }
            laserImpact.transform.localPosition = new Vector3(hit.distance, 0f, 50f);
            laserRenderer.SetPosition(1, new Vector3(50f - laser.transform.localPosition.x, 0f, 0f));
        }

    }

    void UpdateLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, new Vector2(1f, 0f), 50f, layersToHit);

        if (animationTimer <= animationDuration * (laserTicks - laserTickCount) / laserTicks)
        {
            laserTickCount++;

            if (hit.collider != null)
            {
                int damage = 50;
                Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                BossWeapon bossWeapon = hit.transform.gameObject.GetComponent<BossWeapon>();
                BossScript bossScript = hit.transform.gameObject.GetComponent<BossScript>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                else if (bossWeapon != null)
                {
                    bossWeapon.TakeDamage(damage);
                }
                else if (bossScript != null)
                {
                    bossScript.TakeDamage(damage);
                }
            }
        }

        if (hit.collider != null)
        {
            laserImpact.transform.localPosition = new Vector3(hit.distance + 0.5f, 0f, 0f);
            laserRenderer.SetPosition(1, new Vector3(hit.distance + 0.5f - laser.transform.localPosition.x, 0f, 0f));
        }
        else
        {
            laserImpact.transform.localPosition = new Vector3(50f, 0f, 0f);
            laserRenderer.SetPosition(1, new Vector3(50f - laser.transform.localPosition.x, 0f, 0f));
        }
    }
}
