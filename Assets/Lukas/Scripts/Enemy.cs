using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int points = 10;
    public LogicScript logic;
    public SpriteRenderer sprite;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private float audioVolume = 1f;
    public Color damageColor;
    private float currentTimer = 0f;
    private float targetTime = 2f;
    private bool timerEnabled = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
        timerEnabled = true;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, audioVolume);

        Destroy(gameObject);

        logic.AddScore(points);
    }

    void Flicker(float currentTime, float targetTime)
    {
        if (currentTime >= 0f)
        {
            sprite.color = damageColor;
        }
        if (currentTime >= 0.05f)
        {
            sprite.color = Color.white;
        }
        if (currentTime >= 0.1f)
        {
            sprite.color = damageColor;
        }
        if (currentTime >= 0.15f)
        {
            sprite.color = Color.white;
            currentTimer = 0;
            timerEnabled = false;
        }
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timerEnabled)
        {
            currentTimer += Time.deltaTime;
            Flicker(currentTimer, targetTime);
        }
    }
}
