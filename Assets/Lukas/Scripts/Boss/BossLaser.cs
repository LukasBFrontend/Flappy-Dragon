using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [SerializeField] private GameObject laser, laserVortex, laserImpact;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private Texture[] textures;
    [SerializeField] private float chargeDuration = 1f;
    [SerializeField] private float fps = 30f;
    [SerializeField] private float animationDuration = .5f;
    Animator laserVortexAnimator;
    private int animationStep = 0;
    private float fpsCounter, animationTimer, chargeTimer;
    private bool animationIsActive, sequenceIsActive = false;
    private LineRenderer laserRenderer;

    void Start()
    {
        laserVortexAnimator = laserVortex.GetComponent<Animator>();
    }

    void Awake()
    {
        laserRenderer = laser.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (sequenceIsActive)
        {
            chargeTimer -= Time.deltaTime;
        }
        if (chargeTimer <= 0)
        {
            laserVortex.GetComponent<SpriteRenderer>().enabled = false;
            laserVortexAnimator.enabled = false;

            laserRenderer.enabled = true;
            animationIsActive = true;
            animationTimer = animationDuration;
            animationStep = 0;
            fpsCounter = 0;

            laserRenderer.SetPosition(1, new Vector3(-50f, 0f, 0f));

            chargeTimer = chargeDuration;
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
            sequenceIsActive = false;
        }
    }

    public void StartLaserSequence()
    {
        sequenceIsActive = true;

        chargeTimer = chargeDuration;
        animationTimer = animationDuration;

        laserVortex.GetComponent<SpriteRenderer>().enabled = true;
        laserVortexAnimator.enabled = true;
        laserVortexAnimator.SetBool("IsCharging", true);
    }
}
