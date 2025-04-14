using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private AudioClip[] fireClips;
    [SerializeField] private float audioVolume = 1f;
    [SerializeField] private GameObject laser;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private Texture[] textures;
    [SerializeField] private float chargeDuration = 1f;
    [SerializeField] private float fps = 30f;
    [SerializeField] private float animationDuration = .5f;


    private int animationStep = 0;
    private float fpsCounter;
    private float animationTimer;
    private float chargeTimer;
    private float groundMoveSpeed;
    private LineRenderer laserRenderer;
    private bool animationIsActive = false;
    private PlayerScript playerScript;
    private bool playerIsAlive;

    void Start()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        playerIsAlive = playerScript.playerIsAlive;
        chargeTimer = chargeDuration;
        animationTimer = animationDuration;
        groundMoveSpeed = GameObject.FindGameObjectWithTag("Moving").GetComponent<GroundMoveScript>().moveSpeed;
    }

    void Awake()
    {
        laserRenderer = laser.GetComponent<LineRenderer>();
    }
    void Update()
    {
        playerIsAlive = playerScript.playerIsAlive;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            chargeTimer = chargeDuration;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && playerIsAlive && !LogicScript.Instance.isPaused && !EventSystem.current.IsPointerOverGameObject())
        {
            if (chargeTimer > chargeDuration - .3f)
            {
                Shoot();
            }
            else
            {
                ShootLaser();
            }

        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            chargeTimer -= Time.deltaTime;
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
        }

        if (!LogicScript.Instance.isGameOver)
            laserRenderer.SetPosition(1, new Vector3(laserRenderer.GetPosition(1).x - Time.deltaTime * groundMoveSpeed, 0f, 0f));

    }

    void Shoot()
    {
        int random = Random.Range(0, 3);
        SoundFXManager.Instance.playSoundFXClip(fireClips[random], transform, audioVolume);
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
    }

    void ShootLaser()
    {
        laserRenderer.enabled = true;
        animationIsActive = true;
        animationTimer = animationDuration;
        animationStep = 0;
        fpsCounter = 0;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, new Vector2(1f, 0f), 50f, layersToHit);
        if (hit.collider != null)
        {
            Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(100);
            }
            laserRenderer.SetPosition(1, new Vector3(hit.distance, 0f, 0f));
            return;
        }
        laserRenderer.SetPosition(1, new Vector3(50f, 0f, 0f));
    }
}
