using UnityEngine;

public class BluePower : MonoBehaviour
{
    [SerializeField] private float powerUpDuration = 16f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static float powerUpTimer;
    private GameObject player;
    private PlayerScript playerScript;
    private static BluePower instance;

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

        if (this == instance && powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.enabled = false;
            player = other.gameObject;
            powerUpTimer = powerUpDuration;
            playerScript = player.GetComponent<PlayerScript>();
            playerScript.activePowerUp = PowerUp.Blue;
        }
    }
}
