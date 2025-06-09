using UnityEngine;

public class BluePower : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField][Range(0, 100)] private float audioVolume = 50f;
    [SerializeField] private float powerUpDuration = 16f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static float powerUpTimer;
    private GameObject player;
    private PlayerScript playerScript;
    private static BluePower instance;
    private bool isOn, startedFlash = false;
    [HideInInspector] public bool isPreview = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (player == null) return;

        if (powerUpTimer <= 0)
        {
            playerScript.activePowerUp = PowerUp.None;
            Destroy(gameObject);
        }

        if (this == instance && powerUpTimer > 0 && !isPreview)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 2.5f)
            {
                Flash(2.6f, 11);
            }
        }
    }

    public void SetTimer(float t)
    {
        powerUpTimer = t;
    }
    public void Flash(float t, int times)
    {
        if (!startedFlash)
        {
            startedFlash = true;
            InvokeRepeating("OnOff", 0, t / times);
        }
    }

    void OnOff()
    {
        if (!isOn)
        {
            playerScript.activePowerUp = PowerUp.None;
        }
        else
        {
            playerScript.activePowerUp = PowerUp.Blue;
        }
        isOn = !isOn;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && spriteRenderer.enabled)
        {
            SoundFXManager.Instance.playSoundFXClip(audioClip, transform, audioVolume);
            player = other.gameObject;
            powerUpTimer = powerUpDuration;
            playerScript = player.GetComponent<PlayerScript>();
            playerScript.activePowerUp = PowerUp.Blue;
            spriteRenderer.enabled = false;
        }
    }
}
