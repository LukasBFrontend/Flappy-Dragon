using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int points = 10;
    [SerializeField] private AudioClip deathClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    [SerializeField] private Color damageColor;

    [HideInInspector] public bool isActive, isDead = false;
    public bool isFireTarget, isLaserTarget = false;
    private int health;
    private LogicScript logic;
    private SpriteRenderer sprite;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float currentTimer = 0f;
    private float targetTime = 2f;
    private bool timerEnabled = false;

    public void TakeDamage(int damage)
    {
        if (isActive)
        {
            health -= damage;
            timerEnabled = true;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                animator.SetFloat("Health", 100 * health / maxHealth);
            }
        }
    }

    void Die()
    {
        SoundFXManager.Instance.playSoundFXClip(deathClip, transform, audioVolume);

        animator.SetBool("IsDead", true);
        boxCollider.enabled = false;
        isDead = true;

        TextSpawn.Instance.SpawnText('+' + points.ToString(), transform.position);
        logic.AddScore(points);
    }

    void Flicker(float currentTime)
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
        animator = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        health = maxHealth;
    }

    void Update()
    {
        if (timerEnabled)
        {
            currentTimer += Time.deltaTime;
            Flicker(currentTimer);
        }
        if (transform.position.x <= 12)
        {
            isActive = true;
        }
    }
}
