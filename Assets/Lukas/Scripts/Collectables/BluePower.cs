using UnityEngine;

public class BluePower : MonoBehaviour
{
    [SerializeField] private float powerUpDuration = 6f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GameObject player;
    private PlayerScript playerScript;
    void Update()
    {
        if (player == null) return;

        if (powerUpDuration <= 0)
        {
            playerScript.activePowerUp = PowerUp.None;
            Destroy(gameObject);
        }

        powerUpDuration -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.enabled = false;
            player = other.gameObject;
            playerScript = player.GetComponent<PlayerScript>();
            playerScript.activePowerUp = PowerUp.Blue;
        }
    }
}
