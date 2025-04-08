using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int points = 10;
    public LogicScript logic;
    public SpriteRenderer sprite;
    [SerializeField] private AudioClip deathClip;

    public Color damageColor;
    /* public GameObject deathEffect; */
    //Timer
    float currentTimer = 0f;
    float targetTime = 2f;
    bool timerEnabled = false;

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
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, .7f);
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            currentTimer += Time.deltaTime;
            Flicker(currentTimer, targetTime);
        }
    }
}
